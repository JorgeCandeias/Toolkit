namespace Outcompute.Toolkit.Orleans.Compression;

public readonly record struct Compressed<T>
{
    public Compressed(T value, CompressionLevel compressionLevel = CompressionLevel.Optimal)
    {
        Value = value;
        CompressionLevel = compressionLevel;
    }

    public T Value { get; }

    public CompressionLevel CompressionLevel { get; }
}