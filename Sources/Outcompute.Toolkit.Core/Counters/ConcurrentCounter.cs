namespace Outcompute.Toolkit.Core.Counters;

/// <summary>
/// A thread-safe counter.
/// </summary>
/// <remarks>
/// This class is a thin wrapper around <see cref="Interlocked"/> to improve quality-of-life.
/// </remarks>
public class ConcurrentCounter
{
    /// <summary>
    /// Creates a new <see cref="ConcurrentCounter"/> set to zero.
    /// </summary>
    public ConcurrentCounter()
    {
    }

    /// <summary>
    /// Creates a new <see cref="ConcurrentCounter"/> set to specified <paramref name="value"/>.
    /// </summary>
    public ConcurrentCounter(int value)
    {
        _value = value;
    }

    /// <summary>
    /// Holds the current value of the counter.
    /// </summary>
    private int _value;

    /// <summary>
    /// Gets the current value of the counter.
    /// </summary>
    public int Value => _value;

    /// <summary>
    /// Increments the counter, as an atomic operation.
    /// </summary>
    /// <returns>
    /// The incremented value.
    /// </returns>
    public int Increment() => Interlocked.Increment(ref _value);

    /// <summary>
    /// Decrements the counter, as an atomic operation.
    /// </summary>
    /// <returns>
    /// The decremented value.
    /// </returns>
    public int Decrement() => Interlocked.Decrement(ref _value);

    /// <summary>
    /// Sets the counter to a new value, as an atomic operation.
    /// </summary>
    /// <returns>
    /// The original value of the counter.
    /// </returns>
    public int Exchange(int value) => Interlocked.Exchange(ref _value, value);

    /// <summary>
    /// Compares the specified comparand with the counter for equality and, if they are equal, replaces the first value.
    /// </summary>
    /// <returns>
    /// The original value of the counter.
    /// </returns>
    public int CompareExchange(int value, int comparand) => Interlocked.CompareExchange(ref _value, value, comparand);

    /// <summary>
    /// Adds the specified value to the counter, as an atomic operation.
    /// </summary>
    /// <returns>
    /// The new value of the counter.
    /// </returns>
    public int Add(int value) => Interlocked.Add(ref _value, value);

    /// <summary>
    /// Subtracts the specified value from the counter, as an atomic operation.
    /// </summary>
    /// <returns>
    /// The new value of the counter.
    /// </returns>
    public int Subtract(int value) => Interlocked.Add(ref _value, -value);

    /// <summary>
    /// Resets the counter to zero.
    /// </summary>
    /// <returns>
    /// The original value of the counter.
    /// </returns>
    public int Reset() => Interlocked.Exchange(ref _value, 0);
}