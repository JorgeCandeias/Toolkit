namespace Outcompute.Toolkit.Orleans.Compression;

public static class CompressedExtensions
{
    /// <summary>
    /// Wraps the specified <paramref name="value"/> with the <see cref="Compressed{T}"/> marker struct.
    /// </summary>
    public static Compressed<T> AsCompressed<T>(this T value, CompressionLevel compressionLevel = CompressionLevel.Optimal) => new(value, compressionLevel);
}