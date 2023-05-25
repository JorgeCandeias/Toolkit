namespace Outcompute.Toolkit.Time;

/// <summary>
/// Abstracts access to static methods of <see cref="DateTime"/> to facilitate unit testing.
/// </summary>
public interface ISystemClock
{
    /// <inheritdoc cref="DateTime.UtcNow"/>
    DateTime UtcNow { get; }

    /// <inheritdoc cref="DateTime.Now"/>
    DateTime Now { get; }

    /// <inheritdoc cref="DateTime.Today"/>
    DateTime Today { get; }
}