using Microsoft.Extensions.DependencyInjection;

namespace Outcompute.Toolkit.Core.Tests.Time;

public class SystemClockTests
{
    [Fact]
    public void GetsUtcNow()
    {
        // arrange
        var clock = new SystemClock();

        // act
        var now = DateTime.UtcNow;
        var result = clock.UtcNow;

        // assert
        Assert.InRange(result, now.Subtract(TimeSpan.FromSeconds(1)), now.Add(TimeSpan.FromSeconds(1)));
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