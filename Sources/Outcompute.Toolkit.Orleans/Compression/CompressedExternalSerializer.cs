#if NET6_0

using CommunityToolkit.Diagnostics;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using Orleans.Serialization;

namespace Outcompute.Toolkit.Orleans.Compression;

internal class CompressedExternalSerializer : IExternalSerializer
{
    /// <summary>
    /// Caches info on what types are constructed from <see cref="Compressed{T}"/>.
    /// </summary>
    private readonly ConcurrentLazyLookup<Type, bool> _isWrapped = new(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(Compressed<>));

    /// <summary>
    /// Caches the generic argument type of a specified type.
    /// </summary>
    private readonly ConcurrentLazyLookup<Type, Type> _valueTypes = new(x => x.GetGenericArguments().Single());

    /// <summary>
    /// Caches get accessors for <see cref="Compressed{T}.Value"/>.
    /// </summary>
    private readonly ConcurrentLazyLookup<Type, Func<object?, object?>> _valueGetters = new(x => x.GetProperty(nameof(Compressed<int>.Value))!.GetValue);

    /// <summary>
    /// Caches get accessors for <see cref="Compressed{T}.CompressionLevel"/>.
    /// </summary>
    private readonly ConcurrentLazyLookup<Type, Func<object?, CompressionLevel>> _levelGetters = new(x => x.GetProperty(nameof(Compressed<int>.CompressionLevel))!.GetGetMethod()!.CreateDelegate<Func<object?, CompressionLevel>>());

    public bool IsSupportedType(Type itemType)
    {
        Guard.IsNotNull(itemType);

        return _isWrapped[itemType];
    }

    public object DeepCopy(object source, ICopyContext context)
    {
        // get the wrapped value
        var value = _valueGetters[source.GetType()](source);

        // defer the rest to orleans
        return context.DeepCopyInner(value);
    }

    public void Serialize(object item, ISerializationContext context, Type expectedType)
    {
        // get the type
        var type = item.GetType();

        // get the wrapped value
        var value = _valueGetters[type](item);

        // get the compression level
        var level = _levelGetters[type](item);

        // serialize the wrapped value in a nested context
        var writer = new BinaryTokenStreamWriter();
        var local = context.CreateNestedContext(0, writer);
        local.SerializeInner(value);

        // spool the uncompressed into a buffer
        using var uncompressed = new ArrayPoolBufferWriter<byte>();
        foreach (var segment in writer.ToBytes())
        {
            var span = uncompressed.GetSpan(segment.Count);
            segment.AsSpan().CopyTo(span);
            uncompressed.Advance(segment.Count);
        }
        writer.ReleaseBuffers();

        // compress those bytes into another buffer
        var compressed = new ArrayPoolBufferWriter<byte>();
        using (var stream = new GZipStream(compressed.AsStream(), level))
        {
            stream.Write(uncompressed.WrittenSpan);
        }

        // write compressed length prefix
        context.StreamWriter.Write(compressed.WrittenCount);

        // write uncompressed length prefix
        context.StreamWriter.Write(uncompressed.WrittenCount);

        // write compressed data
        foreach (var b in compressed.WrittenSpan)
        {
            context.StreamWriter.Write(b);
        }
    }

    public object Deserialize(Type expectedType, IDeserializationContext context)
    {
        Guard.IsNotNull(expectedType);
        Guard.IsNotNull(context);

        var elementType = _valueTypes[expectedType];

        // read compressed length prefix
        var compressedLength = context.StreamReader.ReadInt();

        // read uncompressed length prefix
        var uncompressedLength = context.StreamReader.ReadInt();

        // read compressed data
        using var compressed = SpanOwner<byte>.Allocate(compressedLength);
        context.StreamReader.ReadByteArray(compressed.DangerousGetArray().Array, 0, compressedLength);

        // decompress the bytes into another buffer
        using var uncompressed = SpanOwner<byte>.Allocate(uncompressedLength);
        using (var sourceWrapper = new MemoryStream(compressed.DangerousGetArray().Array!, 0, compressedLength))
        using (var targetWrapper = new MemoryStream(uncompressed.DangerousGetArray().Array!, 0, uncompressedLength))
        using (var gzip = new GZipStream(sourceWrapper, CompressionMode.Decompress))
        {
            gzip.CopyTo(targetWrapper);
        }

        // defer inner value deserialization
        var reader = new BinaryTokenStreamReader(uncompressed.DangerousGetArray());
        var value = context.CreateNestedContext(0, reader).DeserializeInner(elementType);

        // return the inner value wrapped again
        return typeof(Compressed<>)
            .MakeGenericType(elementType)
            .GetConstructor(new[] { elementType, typeof(CompressionLevel) })!
            .Invoke(null, new[] { value, CompressionLevel.NoCompression })!;
    }
}

#endif