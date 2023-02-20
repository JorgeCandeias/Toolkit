namespace Outcompute.Toolkit.Orleans.Tests.Compression;

public class CompressedAttributeTests
{
    [Fact]
    public void Constructs()
    {
        // arrange
        var level = CompressionLevel.Fastest;

        // act
        var result = new CompressedAttribute(level);

        // assert
        Assert.Equal(level, result.CompressionLevel);
    }
}