using CommunityToolkit.HighPerformance;

namespace Outcompute.Toolkit.HighPerformance.Extensions;

public static class ArrayPoolBufferWriterEnumerableExtensions
{
    /// <summary>
    /// Enumerates the specified <paramref name="source"/> into a new <see cref="ArrayPoolBufferWriter{T}"/> of the correct size.
    /// </summary>
    public static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriter<T>(this IEnumerable<T> source)
    {
        Guard.IsNotNull(source);

        return source switch
        {
            T[] array => ToArrayPoolBufferWriterFromArray(array),
            List<T> list => ToArrayPoolBufferWriterFromList(list),
            Queue<T> queue => ToArrayPoolBufferWriterFromQueue(queue),
            Stack<T> stack => ToArrayPoolBufferWriterFromStack(stack),
            _ => ToArrayPoolBufferWriterFromEnumerable(source)
        };
    }

    /// <summary>
    /// Fast path of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for <see cref="Array"/>.
    /// </summary>
    /// <remarks>
    /// Arrays are internally copied in a single block copy operation.
    /// </remarks>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromArray<T>(T[] source)
    {
        if (source.Length == 0)
        {
            return new ArrayPoolBufferWriter<T>();
        }

        var owner = new ArrayPoolBufferWriter<T>(source.Length);

        var span = owner.GetSpan(source.Length);
        source.CopyTo(span);
        owner.Advance(source.Length);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for <see cref="List{T}"/>.
    /// </summary>
    /// /// <remarks>
    /// Lists are internally copied in a single block copy operation.
    /// </remarks>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromList<T>(List<T> source)
    {
        if (source.Count == 0)
        {
            return new ArrayPoolBufferWriter<T>();
        }

        var owner = new ArrayPoolBufferWriter<T>(source.Count);

        var span = owner.GetSpan(source.Count);
        CollectionsMarshal.AsSpan(source).CopyTo(span);
        owner.Advance(source.Count);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for <see cref="Queue{T}"/>.
    /// </summary>
    /// <remarks>
    /// Queues are internally copied in up to two block copy operations.
    /// </remarks>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromQueue<T>(Queue<T> source)
    {
        if (source.Count == 0)
        {
            return new ArrayPoolBufferWriter<T>();
        }

        var owner = new ArrayPoolBufferWriter<T>(source.Count);

        if (!MemoryMarshal.TryGetArray<T>(owner.GetMemory(source.Count), out var segment))
        {
            // should never happen
            return ThrowHelper.ThrowInvalidOperationException<ArrayPoolBufferWriter<T>>();
        }

        source.CopyTo(segment.Array!, 0);
        owner.Advance(source.Count);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for <see cref="Stack{T}"/>.
    /// </summary>
    /// <remarks>
    /// Stacks are internally copied in a fast while loop.
    /// </remarks>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromStack<T>(Stack<T> source)
    {
        if (source.Count == 0)
        {
            return new ArrayPoolBufferWriter<T>();
        }

        var owner = new ArrayPoolBufferWriter<T>(source.Count);

        if (!MemoryMarshal.TryGetArray<T>(owner.GetMemory(source.Count), out var segment))
        {
            // should never happen
            return ThrowHelper.ThrowInvalidOperationException<ArrayPoolBufferWriter<T>>();
        }

        source.CopyTo(segment.Array!, 0);
        owner.Advance(source.Count);

        return owner;
    }

    /// <summary>
    /// Fallback of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for enumerables.
    /// </summary>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromEnumerable<T>(IEnumerable<T> source)
    {
        if (source.TryGetNonEnumeratedCount(out var count))
        {
            return ToArrayPoolBufferWriterFromEnumerableWithKnownCount(source, count);
        }
        else
        {
            return ToArrayPoolBufferWriterFromEnumerableWithUnknownCount(source);
        }
    }

    /// <summary>
    /// Fallback of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for enumerables with a known count.
    /// </summary>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromEnumerableWithKnownCount<T>(IEnumerable<T> source, int count)
    {
        if (count == 0)
        {
            return new ArrayPoolBufferWriter<T>();
        }

        var owner = new ArrayPoolBufferWriter<T>();
        var span = owner.GetSpan(count);
        var i = 0;

        foreach (var item in source)
        {
            span[i++] = item;
        }

        owner.Advance(count);

        return owner;
    }

    /// <summary>
    /// Fallback of <see cref="ToArrayPoolBufferWriter{T}(IEnumerable{T})"/> for enumerables with an unknown count.
    /// </summary>
    private static ArrayPoolBufferWriter<T> ToArrayPoolBufferWriterFromEnumerableWithUnknownCount<T>(IEnumerable<T> source)
    {
        var owner = new ArrayPoolBufferWriter<T>();

        foreach (var item in source)
        {
            owner.Write(item);
        }

        return owner;
    }
}