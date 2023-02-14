namespace Outcompute.Toolkit.HighPerformance.Extensions;

internal static class BufferUtility
{
    /// <summary>
    /// The starting buffer length to use.
    /// The minimum array size managed by the shared <see cref="ArrayPool{T}"/> is 16.
    /// </summary>
    private const int DefaultBufferLength = 16;

    /// <summary>
    /// Grows the specified <paramref name="buffer"/> using the specified <paramref name="pool"/>.
    /// </summary>
    public static void Grow<T>(this ArrayPool<T> pool, ref T[] buffer, int factor = 2)
    {
        // pin locals
        var length = buffer.Length;

        // break if growth is not possible
        if (length == Array.MaxLength)
        {
            ThrowHelper.ThrowInsufficientMemoryException($"Source contains more than {Array.MaxLength} items");
            return;
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
}