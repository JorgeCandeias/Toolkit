namespace Outcompute.Toolkit.HighPerformance.Extensions;

public static class IMemoryOwnerEnumerableExtensions
{
    /// <summary>
    /// Creates an <see cref="IEnumerable{T}"/> view of the buffer.
    /// </summary>
    /// <remarks>
    /// This method attempts fast paths for <see cref="MemoryOwner{T}"/> and <see cref="ArrayPoolBufferWriter{T}"/> before falling back to <see cref="MemoryMarshal.ToEnumerable{T}(ReadOnlyMemory{T})"/>.
    /// </remarks>
    public static IEnumerable<T> AsEnumerable<T>(this IMemoryOwner<T> source)
    {
        Guard.IsNotNull(source);

        return source switch
        {
            // fast path for well known memory owner
            MemoryOwner<T> owner => owner.AsEnumerable(),

            // fast path for well known buffer writer
            ArrayPoolBufferWriter<T> writer => writer.AsEnumerable(),

            // standard path for any other memory owner
            _ => MemoryMarshal.ToEnumerable<T>(source.Memory)
        };
    }
}