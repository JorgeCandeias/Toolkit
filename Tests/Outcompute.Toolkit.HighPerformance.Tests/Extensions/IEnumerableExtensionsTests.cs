namespace Outcompute.Toolkit.HighPerformance.Tests.Extensions;

public class IEnumerableExtensionsTests
{
    [Fact]
    public void CopiesFromKnownCollection()
    {
        // arrange
        var source = (IEnumerable<int>)new[] { 1, 2, 3 };
        var target = new int[3];

        // act
        source.CopyTo(target, 0);

        // assert
        Assert.Equal(source, target);
    }

    [Fact]
    public void CopiesFromUnknownEnumerable()
    {
        // arrange
        var source = Enumerable.Range(1, 3);
        var target = new int[3];

        // act
        source.CopyTo(target, 0);

        // assert
        Assert.Equal(source, target);
    }
}