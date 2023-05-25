using Microsoft.Extensions.DependencyInjection;

namespace Outcompute.Toolkit.Time;

/// <summary>
/// Default implementation of <see cref="ISystemClock"/>.
/// </summary>
internal class SystemClock : ISystemClock
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => DateTime.Now;

    public DateTime Today => DateTime.Today;
}

public static class SystemClockServiceCollectionExtensions
{
    /// <summary>
    /// Adds the default implementation of <see cref="ISystemClock"/> as a singleton to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    public static IServiceCollection AddSystemClock(this IServiceCollection services)
    {
        Guard.IsNotNull(services);

        return services.AddSingleton<ISystemClock, SystemClock>();
    }
}