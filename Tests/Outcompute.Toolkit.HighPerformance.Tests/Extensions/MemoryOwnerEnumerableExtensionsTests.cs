using CommunityToolkit.HighPerformance;
using Outcompute.Toolkit.HighPerformance.Extensions;

namespace Outcompute.Toolkit.HighPerformance.Tests.Extensions;

public static class MemoryOwnerEnumerableExtensionsTests
{
    [Fact]
    public static void ThrowsOnNullSource()
    {
        // arrange
        IEnumerable<int> source = null!;

        // act
        var ex = Assert.Throws<ArgumentNullException>(() => source.ToMemoryOwner());

        // assert
        Assert.Equal(nameof(source), ex.ParamName);
    }

    [Fact]
    public static void ConvertsArray()
    {
        // arrange
        var source = new[] { 1, 2, 3 };

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Length, result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsList()
    {
        // arrange
        var source = new List<int> { 1, 2, 3 };

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsQueue()
    {
        // arrange
        var source = new Queue<int>();
        for (var i = 1; i <= 3; i++)
        {
            source.Enqueue(i);
        }

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsEnumerableWithKnownCount()
    {
        // arrange
        var source = new HashSet<int> { 1, 2, 3 };

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsEnumerableWithUnknownCount()
    {
        // arrange
        static IEnumerable<int> Generate()
        {
            for (var i = 0; i < 1000; i++)
            {
                yield return i;
            }
        }
        var source = Generate();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count(), result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }
}