namespace Outcompute.Toolkit.Core.InteropServices;

/// <summary>
/// Extra methods related to <see cref="MemoryMarshal"/>.
/// </summary>
public static class MemoryMarshalEx
{
    /// <summary>
    /// Gets an array segment from the underlying memory.
    /// If unable to get the array segment, throws <see cref="InvalidOperationException"/>.
    /// </summary>
    public static ArraySegment<T> GetArray<T>(ReadOnlyMemory<T> memory)
    {
        if (!MemoryMarshal.TryGetArray(memory, out var segment))
        {
            return ThrowHelper.ThrowInvalidOperationException<ArraySegment<T>>("Could not get an array segment from the specified memory block");
        }

        return segment;
    }
}