namespace Outcompute.Toolkit.Orleans.Compression;

/// <summary>
/// Marks the type as elected for compression during serialization.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class CompressedAttribute : Attribute
{
    public CompressedAttribute(CompressionLevel compressionLevel = CompressionLevel.Optimal)
    {
        CompressionLevel = compressionLevel;
    }

    public CompressionLevel CompressionLevel { get; }
}