using CommunityToolkit.Diagnostics;
using CommunityToolkit.HighPerformance.Buffers;
using System.Runtime.InteropServices;

namespace Outcompute.Toolkit.HighPerformance.Extensions;

public static class MemoryOwnerEnumerableExtensions
{
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
            _ => ToMemoryOwnerFromEnumerable(source)
        };
    }

    /// <summary>
    /// Fast path of <see cref="ToMemoryOwner{T}(IEnumerable{T})"/> for arrays.
    /// </summary>
    private static MemoryOwner<T> ToMemoryOwnerFromArray<T>(T[] source)
    {
        var owner = MemoryOwner<T>.Allocate(source.Length);

        source.CopyTo(owner.Span);

        return owner;
    }

    /// <summary>
    /// Fast path of <see cref="ToMemoryOwner{T}(IEnumerable{T})"/> for lists.
    /// </summary>
    private static MemoryOwner<T> ToMemoryOwnerFromList<T>(List<T> source)
    {
        var owner = MemoryOwner<T>.Allocate(source.Count);

        CollectionsMarshal.AsSpan(source).CopyTo(owner.Span);

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
        var owner = MemoryOwner<T>.Allocate(1024);
        var span = owner.Span;
        var i = 0;

        foreach (var item in source)
        {
            if (i == owner.Length)
            {
                var other = MemoryOwner<T>.Allocate(owner.Length * 2);
                owner.Span.CopyTo(other.Span);
                owner.Dispose();
                owner = other;
                span = owner.Span;
            }

            span[i++] = item;
        }

        if (i < owner.Length)
        {
            owner = owner[..i];
        }

        return owner;
    }
}