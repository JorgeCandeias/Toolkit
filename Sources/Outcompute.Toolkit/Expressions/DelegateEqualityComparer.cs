namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Implements an equality comparer from a set of delegate methods.
/// </summary>
/// <typeparam name="TSource"></typeparam>
internal sealed class DelegateEqualityComparer<TSource> : EqualityComparer<TSource>
{
    private readonly HashCodeCalculation<TSource> _hash;
    private readonly EqualityComparison<TSource> _equals;

    internal DelegateEqualityComparer(HashCodeCalculation<TSource> hash, EqualityComparison<TSource> equals)
    {
        Guard.IsNotNull(hash);
        Guard.IsNotNull(equals);

        _hash = hash;
        _equals = equals;
    }

    public override bool Equals(TSource? x, TSource? y) => _equals(x, y);

    public override int GetHashCode(TSource obj) => _hash(obj);
}

/// <summary>
/// Implements an auto-casting <see cref="EqualityComparer{T}"/> from an existing sub-type comparer.
/// </summary>
internal sealed class DelegateEqualityComparer<TSource, TElement> : EqualityComparer<TSource>
    where TElement : TSource
{
    private readonly EqualityComparer<TElement> _comparer;

    public DelegateEqualityComparer(EqualityComparer<TElement> comparer)
    {
        Guard.IsNotNull(comparer);

        _comparer = comparer;
    }

    public override bool Equals(TSource? x, TSource? y)
    {
        var elementX = (TElement)x!;
        var elementY = (TElement)y!;

        return _comparer.Equals(elementX, elementY);
    }

    public override int GetHashCode(TSource obj)
    {
        var element = (TElement)obj!;

        return _comparer.GetHashCode(element);
    }
}

/// <summary>
/// Unified factory methods for delegate equality comparers.
/// </summary>
internal static class DelegateEqualityComparer
{
    /// <summary>
    /// Creates a new <see cref="DelegateEqualityComparer{TSource}"/> that delegates hash calculation and equality comparison to the specified delegates.
    /// </summary>
    public static DelegateEqualityComparer<TSource> Create<TSource>(HashCodeCalculation<TSource> hash, EqualityComparison<TSource> equals) => new(hash, equals);

    /// <summary>
    /// Creates a new <see cref="DelegateEqualityComparer{TSource, TElement}"/> that delegates comparison to the specified comparer for a derived element.
    /// </summary>
    public static DelegateEqualityComparer<TSource, TElement> Create<TSource, TElement>(EqualityComparer<TElement> comparer) where TElement : TSource => new(comparer);
}