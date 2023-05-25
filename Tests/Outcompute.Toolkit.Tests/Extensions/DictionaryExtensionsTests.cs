using Outcompute.Toolkit.Extensions;

namespace Outcompute.Toolkit.Tests.Extensions;

public class DictionaryExtensionsTests
{
    [Fact]
    public void AddsRangeFromKeyValuePairs()
    {
        // arrange
        var dictionary = new Dictionary<int, string>();
        var items = new[] {
            new KeyValuePair<int, string>(1, "A"),
            new KeyValuePair<int, string>(2, "B"),
            new KeyValuePair<int, string>(3, "C"),
        };

        // act
        dictionary.AddRange(items);

        // assert
        Assert.Equal(items, dictionary.OrderBy(x => x.Key));
    }

    [Fact]
    public void AddsRangeFromTuples()
    {
        // arrange
        var dictionary = new Dictionary<int, string>();
        var items = new[] { (1, "A"), (2, "B"), (3, "C") };

        // act
        dictionary.AddRange(items);

        // assert
        Assert.Equal(items, dictionary.OrderBy(x => x.Key).Select(x => (x.Key, x.Value)));
    }

    [Fact]
    public void ReplacesFromKeyValuePairs()
    {
        // arrange
        var dictionary = new Dictionary<int, string>
        {
            { 1, "A" }, { 2, "B"}, { 3, "C" }
        };
        var items = new[] {
            new KeyValuePair<int, string>(4, "A"),
            new KeyValuePair<int, string>(5, "B"),
            new KeyValuePair<int, string>(6, "C"),
        };

        // act
        dictionary.ReplaceWith(items);

        // assert
        Assert.Equal(items, dictionary.OrderBy(x => x.Key));
    }

    [Fact]
    public void ReplacesFromTuples()
    {
        // arrange
        var dictionary = new Dictionary<int, string>
        {
            { 1, "A" }, { 2, "B"}, { 3, "C" }
        };
        var items = new[] { (4, "A"), (5, "B"), (6, "C") };

        // act
        dictionary.ReplaceWith(items);

        // assert
        Assert.Equal(items, dictionary.OrderBy(x => x.Key).Select(x => (x.Key, x.Value)));
    }

    [Fact]
    public void GetOrAddNewReturnsNewForNewKey()
    {
        // arrange
        var dictionary = new Dictionary<int, List<int>>();

        // act
        var result = dictionary.GetOrAddNew(1);

        // assert
        var pair = Assert.Single(dictionary);
        Assert.Equal(1, pair.Key);
        Assert.Same(result, pair.Value);
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}