namespace Outcompute.Toolkit.Orleans.Tests.Compression;

public class CompressedTests
{
    [Fact]
    public void Constructs()
    {
        // arrange
        var value = 123;
        var level = CompressionLevel.Fastest;

        // act
        var result = new Compressed<int>(value, level);

        // assert
        Assert.Equal(value, result.Value);
        Assert.Equal(level, result.CompressionLevel);
    }
}