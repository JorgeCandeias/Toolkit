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

        return source switch
        {
            T[] array => ToMemoryOwnerFromArray(array),
            List<T> list => ToMemoryOwnerFromList(list),
            Queue<T> queue => ToMemoryOwnerFromQueue(queue),
            Stack<T> stack => ToMemoryOwnerFromStack(stack),
            _ => ToMemoryOwnerFromEnumerable(source)
        };
    }

    /// <summary>
    /// Fast path of <see cref="ToMemoryOwner{T}(IEnumerable{T})"/> for <see cref="Array"/>.
    /// </summary>
    /// <remarks>
    /// Arrays are internally copied in a single block copy operation.
    /// </remarks>
    private static MemoryOwner<T> ToMemoryOwnerFromArray<T>(T[] source)
    {
        if (source.Length == 0)
        {
            return MemoryOwner<T>.Empty;
        }

        var owner = MemoryOwner<T>.Allocate(source.Length);

        source.CopyTo(owner.Span);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToMemoryOwner{T}(IEnumerable{T})"/> for <see cref="List{T}"/>.
    /// </summary>
    /// /// <remarks>
    /// Lists are internally copied in a single block copy operation.
    /// </remarks>
    private static MemoryOwner<T> ToMemoryOwnerFromList<T>(List<T> source)
    {
        if (source.Count == 0)
        {
            return MemoryOwner<T>.Empty;
        }

        var owner = MemoryOwner<T>.Allocate(source.Count);
        var target = owner.DangerousGetArray().Array!;

        source.CopyTo(target);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToMemoryOwner{T}(IEnumerable{T})"/> for <see cref="Queue{T}"/>.
    /// </summary>
    /// <remarks>
    /// Queues are internally copied in up to two block copy operations.
    /// </remarks>
    private static MemoryOwner<T> ToMemoryOwnerFromQueue<T>(Queue<T> source)
    {
        if (source.Count == 0)
        {
            return MemoryOwner<T>.Empty;
        }

        var owner = MemoryOwner<T>.Allocate(source.Count);
        var target = owner.DangerousGetArray().Array!;
        source.CopyTo(target, 0);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToMemoryOwner{T}(IEnumerable{T})"/> for <see cref="Stack{T}"/>.
    /// </summary>
    /// <remarks>
    /// Stacks are internally copied in a fast while loop.
    /// </remarks>
    private static MemoryOwner<T> ToMemoryOwnerFromStack<T>(Stack<T> source)
    {
        if (source.Count == 0)
        {
            return MemoryOwner<T>.Empty;
        }

        var owner = MemoryOwner<T>.Allocate(source.Count);
        var target = owner.DangerousGetArray().Array!;
        source.CopyTo(target, 0);

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
                temp = Grow(temp, i);
            }

            temp[i++] = item;
        }

        // copy the temp buffer into the memory owner
        var owner = MemoryOwner<T>.Allocate(i);
        temp.AsSpan(..i).CopyTo(owner.Span);
        ArrayPool<T>.Shared.Return(temp);

        return owner;

        // grows the temp buffer
        static T[] Grow(T[] temp, int i)
        {
            // break if growth is not possible
            if (i == Array.MaxLength)
            {
                return ThrowHelper.ThrowInsufficientMemoryException<T[]>($"Source contains more than {Array.MaxLength} items");
            }

            // double the length while clamping overflow
            var length = (int)Math.Min((uint)temp.Length * 2, int.MaxValue);

            // grow to at least the first bucket size above zero
            length = Math.Max(length, DefaultBufferLength);

            // grow to at most the max size of an array
            length = Math.Min(length, Array.MaxLength);

            // swap the buffers
            var other = ArrayPool<T>.Shared.Rent(length);
            Array.Copy(temp, other, i);
            ArrayPool<T>.Shared.Return(temp);

            return other;
        }
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

    /// <summary>
    /// Creates an <see cref="IEnumerable{T}"/> view of the buffer.
    /// </summary>
    /// <remarks>
    /// This method provides higher performance than the standard <see cref="MemoryMarshal.ToEnumerable{T}(ReadOnlyMemory{T})"/> for <see cref="MemoryOwner{T}"/> instances.
    /// </remarks>
    public static IEnumerable<T> AsEnumerable<T>(this MemoryOwner<T> source)
    {
        Guard.IsNotNull(source);

        var length = source.Length;

        for (var i = 0; i < length; i++)
        {
            yield return source.Span[i];
        }
    }
}