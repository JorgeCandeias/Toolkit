namespace Outcompute.Toolkit.HighPerformance.Extensions;

internal class BufferUtility
{
    internal BufferUtility(int arrayMaxLength)
    {
        Guard.IsGreaterThanOrEqualTo(arrayMaxLength, 0);

        _arrayMaxLength = arrayMaxLength;
    }

    /// <summary>
    /// The starting buffer length to use.
    /// The minimum array size managed by the shared <see cref="ArrayPool{T}"/> is 16.
    /// </summary>
    private const int DefaultBufferLength = 16;

    /// <summary>
    /// The max length of an array.
    /// </summary>
    private readonly int _arrayMaxLength;

    /// <summary>
    /// Grows the specified <paramref name="buffer"/> using the specified <paramref name="pool"/>.
    /// </summary>
    public void Grow<T>(ArrayPool<T> pool, ref T[] buffer, int factor)
    {
        // pin locals
        var length = buffer.Length;

        // break if growth is not possible
        if (length >= _arrayMaxLength)
        {
            ThrowHelper.ThrowInsufficientMemoryException($"Source contains more than {Array.MaxLength} items");
        }

        // double the length while clamping overflow
        var newLength = (int)Math.Min((uint)length * factor, int.MaxValue);

        // grow to at least the first bucket size above zero
        newLength = Math.Max(newLength, DefaultBufferLength);

        // grow to at most the max size of an array
        newLength = Math.Min(newLength, Array.MaxLength);

        // swap the buffers
        var other = pool.Rent(newLength);
        Array.Copy(buffer, other, length);
        pool.Return(buffer);
        buffer = other;
    }

    /// <summary>
    /// The shared buffer utility with default settings.
    /// </summary>
    public static BufferUtility Shared { get; } = new BufferUtility(Array.MaxLength);
}

internal static class BufferUtilityExtensions
{
    public static void Grow<T>(this ArrayPool<T> pool, ref T[] buffer, int factor = 2)
    {
        Guard.IsNotNull(pool);
        Guard.IsNotNull(buffer);
        Guard.IsGreaterThan(factor, 1);

        BufferUtility.Shared.Grow(pool, ref buffer, factor);
    }
}