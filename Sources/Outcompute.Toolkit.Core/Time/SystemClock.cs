namespace Outcompute.Toolkit.Core.Time;

/// <summary>
/// Abstracts access to static methods of <see cref="DateTime"/> to facilitate unit testing.
/// </summary>
public interface ISystemClock
{
    /// <inheritdoc cref="DateTime.UtcNow"/>
    DateTime UtcNow { get; }
}

/// <summary>
/// Default implementation of <see cref="ISystemClock"/>.
/// </summary>
internal class SystemClock : ISystemClock
{
    public DateTime UtcNow => DateTime.UtcNow;
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