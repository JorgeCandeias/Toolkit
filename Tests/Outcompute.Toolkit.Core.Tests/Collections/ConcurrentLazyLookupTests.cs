using System.Collections;

namespace Outcompute.Toolkit.Core.Tests.Collections;

public class ConcurrentLazyLookupTests
{
    [Fact]
    public void AddsItemsWithIndexing()
    {
        // arrange
        var lookup = new ConcurrentLazyLookup<int, string>(x => x.ToString());
        var expected = new[] { "1", "2", "3" };

        // act
        var r1 = lookup[1];
        var r2 = lookup[2];
        var r3 = lookup[3];

        // assert
        Assert.Equal(expected, new[] { r1, r2, r3 });
        Assert.Equal(3, lookup.Count);
        Assert.Equal(new[] { 1, 2, 3 }, lookup.Keys.Order());
        Assert.Equal(expected, lookup.Values.Order());
        Assert.Equal(new[] { (1, "1"), (2, "2"), (3, "3") }, lookup.Select(x => (x.Key, x.Value)));
        Assert.True(lookup.ContainsKey(1));
        Assert.True(lookup.ContainsKey(2));
        Assert.True(lookup.ContainsKey(3));
        Assert.False(lookup.ContainsKey(4));

        IEnumerable legacy = lookup;
        Assert.Equal(new[] { (1, "1"), (2, "2"), (3, "3") }, legacy.Cast<KeyValuePair<int, string>>().Select(x => (x.Key, x.Value)));
    }

    [Fact]
    public void AddsItemsWithTryGetValue()
    {
        // arrange
        var lookup = new ConcurrentLazyLookup<int, string>(x => x.ToString());
        var expected = new[] { "1", "2", "3" };

        // act
        var r1 = lookup.TryGetValue(1, out var v1);
        var r2 = lookup.TryGetValue(2, out var v2);
        var r3 = lookup.TryGetValue(3, out var v3);

        // assert
        Assert.Equal(new[] { true, true, true }, new[] { r1, r2, r3 });
        Assert.Equal(expected, new[] { v1, v2, v3 });
        Assert.Equal(3, lookup.Count);
        Assert.Equal(new[] { 1, 2, 3 }, lookup.Keys.Order());
        Assert.Equal(expected, lookup.Values.Order());
        Assert.Equal(new[] { (1, "1"), (2, "2"), (3, "3") }, lookup.Select(x => (x.Key, x.Value)));
        Assert.True(lookup.ContainsKey(1));
        Assert.True(lookup.ContainsKey(2));
        Assert.True(lookup.ContainsKey(3));
        Assert.False(lookup.ContainsKey(4));

        IEnumerable legacy = lookup;
        Assert.Equal(new[] { (1, "1"), (2, "2"), (3, "3") }, legacy.Cast<KeyValuePair<int, string>>().Select(x => (x.Key, x.Value)));
    }
}