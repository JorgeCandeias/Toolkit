namespace Outcompute.Toolkit.Core.Tests.Time;

public class SystemClockTests
{
    [Fact]
    public void GetsUtcNow()
    {
        // arrange
        var clock = new SystemClock();

        // act
        var expected = DateTime.UtcNow;
        var result = clock.UtcNow;

        // assert
        Assert.InRange(result, expected.Subtract(TimeSpan.FromSeconds(1)), expected.Add(TimeSpan.FromSeconds(1)));
    }

    [Fact]
    public void GetsNow()
    {
        // arrange
        var clock = new SystemClock();

        // act
        var expected = DateTime.Now;
        var result = clock.Now;

        // assert
        Assert.InRange(result, expected.Subtract(TimeSpan.FromSeconds(1)), expected.Add(TimeSpan.FromSeconds(1)));
    }

    [Fact]
    public void GetsToday()
    {
        // arrange
        var clock = new SystemClock();

        // act
        var expected = DateTime.Today;
        var result = clock.Today;

        // assert
        Assert.InRange(result, expected.Subtract(TimeSpan.FromSeconds(1)), expected.Add(TimeSpan.FromSeconds(1)));
    }

    [Fact]
    public void AddsToServiceCollection()
    {
        // arrange
        var services = new ServiceCollection();

        // act
        var result = services.AddSystemClock();

        // assert
        Assert.Same(services, result);
        Assert.NotEmpty(services);
    }
}