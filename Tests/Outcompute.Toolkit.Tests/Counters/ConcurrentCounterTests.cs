using Outcompute.Toolkit.Counters;

namespace Outcompute.Toolkit.Tests.Counters;

public class ConcurrentCounterTests
{
    [Fact]
    public void InitializesWithZero()
    {
        // act
        var counter = new ConcurrentCounter();

        // assert
        Assert.Equal(0, counter.Value);
    }

    [Fact]
    public void InitializesWithValue()
    {
        // act
        var counter = new ConcurrentCounter(123);

        // assert
        Assert.Equal(123, counter.Value);
    }

    [Fact]
    public void Increments()
    {
        // arrange
        var counter = new ConcurrentCounter();

        // act
        var r1 = counter.Increment();
        var r2 = counter.Increment();
        var r3 = counter.Increment();
        var value = counter.Value;

        // assert
        Assert.Equal(new[] { 1, 2, 3, 3 }, new[] { r1, r2, r3, value });
    }

    [Fact]
    public void Decrements()
    {
        // arrange
        var counter = new ConcurrentCounter();

        // act
        var r1 = counter.Decrement();
        var r2 = counter.Decrement();
        var r3 = counter.Decrement();
        var value = counter.Value;

        // assert
        Assert.Equal(new[] { -1, -2, -3, -3 }, new[] { r1, r2, r3, value });
    }

    [Fact]
    public void Exchanges()
    {
        // arrange
        var counter = new ConcurrentCounter();

        // act
        var r1 = counter.Exchange(123);
        var r2 = counter.Exchange(234);
        var r3 = counter.Exchange(345);
        var value = counter.Value;

        // assert
        Assert.Equal(new[] { 0, 123, 234, 345 }, new[] { r1, r2, r3, value });
    }

    [Fact]
    public void CompareExchanges()
    {
        // arrange
        var counter = new ConcurrentCounter();

        // act
        var r1 = counter.CompareExchange(123, 0);
        var r2 = counter.CompareExchange(234, 0);
        var value = counter.Value;

        // assert
        Assert.Equal(new[] { 0, 123, 123 }, new[] { r1, r2, value });
    }

    [Fact]
    public void Adds()
    {
        // arrange
        var counter = new ConcurrentCounter();

        // act
        var r1 = counter.Add(123);
        var r2 = counter.Add(234);
        var r3 = counter.Add(345);
        var value = counter.Value;

        // assert
        Assert.Equal(new[] { 123, 357, 702, 702 }, new[] { r1, r2, r3, value });
    }

    [Fact]
    public void Subtracts()
    {
        // arrange
        var counter = new ConcurrentCounter();

        // act
        var r1 = counter.Subtract(123);
        var r2 = counter.Subtract(234);
        var r3 = counter.Subtract(345);
        var value = counter.Value;

        // assert
        Assert.Equal(new[] { -123, -357, -702, -702 }, new[] { r1, r2, r3, value });
    }

    [Fact]
    public void Resets()
    {
        // arrange
        var counter = new ConcurrentCounter(123);

        // act
        var r1 = counter.Reset();
        var value = counter.Value;

        // assert
        Assert.Equal(new[] { 123, 0 }, new[] { r1, value });
    }
}