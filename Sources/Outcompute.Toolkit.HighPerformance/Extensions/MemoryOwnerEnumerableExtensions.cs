using Outcompute.Toolkit.Core.InteropServices;

namespace Outcompute.Toolkit.HighPerformance.Extensions;

public static class MemoryOwnerEnumerableExtensions
{
    /// <summary>
    /// The starting buffer length to use.
    /// The minimum array size managed by the shared <see cref="ArrayPool{T}"/> is 16.
    /// </summary>
    private const int DefaultBufferLength = 16;

    /// <summary>
    /// Enumerates the specified <paramref name="source"/> into a new <see cref="MemoryOwner{T}"/> of the correct size.
    /// </summary>
    public static MemoryOwner<T> ToMemoryOwner<T>(this IEnumerable<T> source)
    {
        Guard.IsNotNull(source);

        // attempt fast path for collection type
        if (source is ICollection<T> collection)
        {
            return ToMemoryOwnerFromCollection(collection);
        }

        // fallback for other enumerables
        return ToMemoryOwnerFromEnumerable(source);
    }

    /// <summary>
    /// Copies the specified <paramref name="source"/> into a new <see cref="MemoryOwner{T}"/> of the correct size.
    /// </summary>
    public static MemoryOwner<T> ToMemoryOwner<T>(this Span<T> source)
    {
        var owner = MemoryOwner<T>.Allocate(source.Length);

        source.CopyTo(owner.Span);

        return owner;
    }

    /// <summary>
    /// Copies the specified <paramref name="source"/> into a new <see cref="MemoryOwner{T}"/> of the correct size.
    /// </summary>
    public static MemoryOwner<T> ToMemoryOwner<T>(this ReadOnlySpan<T> source)
    {
        var owner = MemoryOwner<T>.Allocate(source.Length);

        source.CopyTo(owner.Span);

        return owner;
    }

    private static MemoryOwner<T> ToMemoryOwnerFromCollection<T>(ICollection<T> collection)
    {
        var count = collection.Count;
        var owner = MemoryOwner<T>.Allocate(count);
        var memory = owner.Memory;
        var segment = MemoryMarshalEx.GetArray<T>(memory);

        collection.CopyTo(segment.Array!, 0);

        return owner;
    }

    /// <summary>
    /// Fallback of <see cref="ToMemoryOwner{T}(IEnumerable{T})"/> for enumerables.
    /// </summary>
    private static MemoryOwner<T> ToMemoryOwnerFromEnumerable<T>(IEnumerable<T> source)
    {
        if (source.TryGetNonEnumeratedCount(out var count))
        {
            return ToMemoryOwnerFromEnumerableWithKnownCount(source, count);
        }
        else
        {
            return ToMemoryOwnerFromEnumerableWithUnknownCount(source);
        }
    }

    /// <summary>
    /// Fallback of <see cref="ToMemoryOwner{T}(IEnumerable{T})"/> for enumerables with a known count.
    /// </summary>
    private static MemoryOwner<T> ToMemoryOwnerFromEnumerableWithKnownCount<T>(IEnumerable<T> source, int count)
    {
        if (count == 0)
        {
            return MemoryOwner<T>.Empty;
        }

        var owner = MemoryOwner<T>.Allocate(count);
        var span = owner.Span;
        var i = 0;

        foreach (var item in source)
        {
            span[i++] = item;
        }

        return owner;
    }

    /// <summary>
    /// Fallback of <see cref="ToMemoryOwner{T}(IEnumerable{T})"/> for enumerables with an unknown count.
    /// </summary>
    private static MemoryOwner<T> ToMemoryOwnerFromEnumerableWithUnknownCount<T>(IEnumerable<T> source)
    {
        // grab the starting buffer
        var temp = ArrayPool<T>.Shared.Rent(DefaultBufferLength);
        var i = 0;

        // enumerate while growing the buffer
        foreach (var item in source)
        {
            // grow the buffer as needed
            if (i == temp.Length)
            {
                ArrayPool<T>.Shared.Grow(ref temp);
            }

            temp[i++] = item;
        }

        // copy the temp buffer into the memory owner
        var owner = MemoryOwner<T>.Allocate(i);
        temp.AsSpan(..i).CopyTo(owner.Span);
        ArrayPool<T>.Shared.Return(temp);

        return owner;
    }

    /// <summary>
    /// Creates an <see cref="IEnumerable{T}"/> view of the buffer.
    /// </summary>
    /// <remarks>
    /// This method provides higher performance than the standard <see cref="MemoryMarshal.ToEnumerable{T}(ReadOnlyMemory{T})"/> for <see cref="MemoryOwner{T}"/> instances.
    /// </remarks>
    public static IEnumerable<T> AsEnumerable<T>(this MemoryOwner<T> source)
    {
        Guard.IsNotNull(source);

        var segment = MemoryMarshalEx.GetArray<T>(source.Memory);

        foreach (var item in segment)
        {
            yield return item;
        }
    }
}