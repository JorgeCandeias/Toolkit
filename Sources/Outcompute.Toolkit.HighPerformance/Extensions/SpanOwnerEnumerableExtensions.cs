namespace Outcompute.Toolkit.HighPerformance.Extensions;

public static class SpanOwnerEnumerableExtensions
{
    /// <summary>
    /// The starting buffer length to use.
    /// The minimum array size managed by the shared <see cref="ArrayPool{T}"/> is 16.
    /// </summary>
    private const int DefaultBufferLength = 16;

    /// <summary>
    /// Enumerates the specified <paramref name="source"/> into a new <see cref="SpanOwner{T}{T}"/> of the correct size.
    /// </summary>
    public static SpanOwner<T> ToSpanOwner<T>(this IEnumerable<T> source)
    {
        Guard.IsNotNull(source);

        return source switch
        {
            T[] array => ToSpanOwnerFromArray(array),
            List<T> list => ToSpanOwnerFromList(list),
            Queue<T> queue => ToSpanOwnerFromQueue(queue),
            Stack<T> stack => ToSpanOwnerFromStack(stack),
            _ => ToSpanOwnerFromEnumerable(source)
        };
    }

    /// <summary>
    /// Copies the specified <paramref name="source"/> into a new <see cref="SpanOwner{T}"/> of the correct size.
    /// </summary>
    public static SpanOwner<T> ToSpanOwner<T>(this Span<T> source)
    {
        var owner = SpanOwner<T>.Allocate(source.Length);

        source.CopyTo(owner.Span);

        return owner;
    }

    /// <summary>
    /// Copies the specified <paramref name="source"/> into a new <see cref="SpanOwner{T}"/> of the correct size.
    /// </summary>
    public static SpanOwner<T> ToSpanOwner<T>(this ReadOnlySpan<T> source)
    {
        var owner = SpanOwner<T>.Allocate(source.Length);

        source.CopyTo(owner.Span);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToSpanOwner{T}(IEnumerable{T})"/> for <see cref="Array"/>.
    /// </summary>
    /// <remarks>
    /// Arrays are internally copied in a single block copy operation.
    /// </remarks>
    private static SpanOwner<T> ToSpanOwnerFromArray<T>(T[] source)
    {
        if (source.Length == 0)
        {
            return SpanOwner<T>.Empty;
        }

        var owner = SpanOwner<T>.Allocate(source.Length);

        source.CopyTo(owner.Span);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToSpanOwner{T}(IEnumerable{T})"/> for <see cref="List{T}"/>.
    /// </summary>
    /// /// <remarks>
    /// Lists are internally copied in a single block copy operation.
    /// </remarks>
    private static SpanOwner<T> ToSpanOwnerFromList<T>(List<T> source)
    {
        if (source.Count == 0)
        {
            return SpanOwner<T>.Empty;
        }

        var owner = SpanOwner<T>.Allocate(source.Count);
        var target = owner.DangerousGetArray().Array!;

        source.CopyTo(target);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToSpanOwner{T}(IEnumerable{T})"/> for <see cref="Queue{T}"/>.
    /// </summary>
    /// <remarks>
    /// Queues are internally copied in up to two block copy operations.
    /// </remarks>
    private static SpanOwner<T> ToSpanOwnerFromQueue<T>(Queue<T> source)
    {
        if (source.Count == 0)
        {
            return SpanOwner<T>.Empty;
        }

        var owner = SpanOwner<T>.Allocate(source.Count);
        var target = owner.DangerousGetArray().Array!;
        source.CopyTo(target, 0);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToSpanOwner{T}(IEnumerable{T})"/> for <see cref="Stack{T}"/>.
    /// </summary>
    /// <remarks>
    /// Stacks are internally copied in a fast while loop.
    /// </remarks>
    private static SpanOwner<T> ToSpanOwnerFromStack<T>(Stack<T> source)
    {
        if (source.Count == 0)
        {
            return SpanOwner<T>.Empty;
        }

        var owner = SpanOwner<T>.Allocate(source.Count);
        var target = owner.DangerousGetArray().Array!;
        source.CopyTo(target, 0);

        return owner;
    }

    /// <summary>
    /// Fallback of <see cref="ToSpanOwner{T}(IEnumerable{T})"/> for enumerables.
    /// </summary>
    private static SpanOwner<T> ToSpanOwnerFromEnumerable<T>(IEnumerable<T> source)
    {
        if (source.TryGetNonEnumeratedCount(out var count))
        {
            return ToSpanOwnerFromEnumerableWithKnownCount(source, count);
        }
        else
        {
            return ToSpanOwnerFromEnumerableWithUnknownCount(source);
        }
    }

    /// <summary>
    /// Fallback of <see cref="ToSpanOwner{T}(IEnumerable{T})"/> for enumerables with a known count.
    /// </summary>
    private static SpanOwner<T> ToSpanOwnerFromEnumerableWithKnownCount<T>(IEnumerable<T> source, int count)
    {
        if (count == 0)
        {
            return SpanOwner<T>.Empty;
        }

        var owner = SpanOwner<T>.Allocate(count);
        var span = owner.Span;
        var i = 0;

        foreach (var item in source)
        {
            span[i++] = item;
        }

        return owner;
    }

    /// <summary>
    /// Fallback of <see cref="ToSpanOwner{T}(IEnumerable{T})"/> for enumerables with an unknown count.
    /// </summary>
    private static SpanOwner<T> ToSpanOwnerFromEnumerableWithUnknownCount<T>(IEnumerable<T> source)
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
        var owner = SpanOwner<T>.Allocate(i);
        temp.AsSpan(..i).CopyTo(owner.Span);
        ArrayPool<T>.Shared.Return(temp);

        return owner;
    }
}