namespace Outcompute.Toolkit.Orleans.Tests.Compression;

public class CompressedExtensionsTests
{
    [Fact]
    public void Constructs()
    {
        // arrange
        var value = 123;
        var level = CompressionLevel.Fastest;

        // act
        var result = value.AsCompressed(level);

        // assert
        Assert.Equal(value, result.Value);
        Assert.Equal(level, result.CompressionLevel);
    }
}