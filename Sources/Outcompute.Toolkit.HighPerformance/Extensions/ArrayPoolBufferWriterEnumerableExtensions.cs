using Outcompute.Toolkit.Core.InteropServices;

namespace Outcompute.Toolkit.HighPerformance.Extensions;

public static class ArrayPoolBufferWriterEnumerableExtensions
{
    /// <summary>
    /// Enumerates the specified <paramref name="source"/> into a new <see cref="ArrayPoolBufferWriter{T}"/> of the correct size.
    /// </summary>
    public static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriter<T>(this IEnumerable<T> source)
    {
        Guard.IsNotNull(source);

        // attempt fast path for collection type
        if (source is ICollection<T> collection)
        {
            return ToArrayPoolBufferWriterFromCollection(collection);
        }

        // fallback for other enumerables
        return ToArrayPoolBufferWriterFromEnumerable(source);
    }

    /// <summary>
    /// Copies the specified <paramref name="source"/> into a new <see cref="ArrayPoolBufferWriter{T}"/> of the correct size.
    /// </summary>
    public static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriter<T>(this Span<T> source)
    {
        var length = source.Length;

        var owner = new ArrayPoolBufferWriter<T>(length);
        var buffer = owner.GetSpan(length);
        source.CopyTo(buffer);
        owner.Advance(length);

        return owner;
    }

    /// <summary>
    /// Copies the specified <paramref name="source"/> into a new <see cref="ArrayPoolBufferWriter{T}"/> of the correct size.
    /// </summary>
    public static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriter<T>(this ReadOnlySpan<T> source)
    {
        var length = source.Length;

        var owner = new ArrayPoolBufferWriter<T>(length);
        var buffer = owner.GetSpan(length);
        source.CopyTo(buffer);
        owner.Advance(length);

        return owner;
    }

    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromCollection<T>(ICollection<T> collection)
    {
        var count = collection.Count;
        var owner = new ArrayPoolBufferWriter<T>(count);
        var memory = owner.GetMemory(count);
        var segment = MemoryMarshalEx.GetArray<T>(memory);

        collection.CopyTo(segment.Array!, 0);

        owner.Advance(count);

        return owner;
    }

    /// <summary>
    /// Fallback of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for enumerables.
    /// </summary>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromEnumerable<T>(IEnumerable<T> source)
    {
        if (source.TryGetNonEnumeratedCount(out var count))
        {
            return ToArrayPoolBufferWriterFromEnumerableWithKnownCount(source, count);
        }
        else
        {
            return ToArrayPoolBufferWriterFromEnumerableWithUnknownCount(source);
        }
    }

    /// <summary>
    /// Fallback of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for enumerables with a known count.
    /// </summary>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromEnumerableWithKnownCount<T>(IEnumerable<T> source, int count)
    {
        if (count == 0)
        {
            return new ArrayPoolBufferWriter<T>();
        }

        var owner = new ArrayPoolBufferWriter<T>();
        var span = owner.GetSpan(count);
        var i = 0;

        foreach (var item in source)
        {
            span[i++] = item;
        }

        owner.Advance(count);

        return owner;
    }

    /// <summary>
    /// Fallback of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for enumerables with an unknown count.
    /// </summary>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromEnumerableWithUnknownCount<T>(IEnumerable<T> source)
    {
        var owner = new ArrayPoolBufferWriter<T>();

        foreach (var item in source)
        {
            owner.Write(item);
        }

        return owner;
    }

    /// <summary>
    /// Creates an <see cref="IEnumerable{T}"/> view of the buffer.
    /// </summary>
    /// <remarks>
    /// This method provides higher performance than the standard <see cref="MemoryMarshal.ToEnumerable{T}(ReadOnlyMemory{T})"/> for <see cref="ArrayPoolBufferWriter{T}"/> instances.
    /// </remarks>
    public static IEnumerable<T> AsEnumerable<T>(this ArrayPoolBufferWriter<T> source)
    {
        Guard.IsNotNull(source);

        var segment = MemoryMarshalEx.GetArray(source.WrittenMemory);

        foreach (var item in segment)
        {
            yield return item;
        }
    }
}