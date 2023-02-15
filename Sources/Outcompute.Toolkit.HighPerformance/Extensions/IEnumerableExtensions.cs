namespace Outcompute.Toolkit.HighPerformance.Extensions;

/// <summary>
/// Quality-of-life extensions for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Copies the items in <paramref name="source"/> to specified <paramref name="target"/>.
    /// </summary>
    public static void CopyTo<T>(this IEnumerable<T> source, T[] target, int arrayIndex)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(target);
        Guard.IsGreaterThanOrEqualTo(arrayIndex, 0);

        // attempt fast path for generic collection
        if (source is ICollection<T> collection)
        {
            collection.CopyTo(target, arrayIndex);
            return;
        }

        // fallback for other enumerables
        var i = 0;
        foreach (var item in source)
        {
            target[i++] = item;
        }
    }
}