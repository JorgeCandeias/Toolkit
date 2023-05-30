using System.Reflection;

namespace Outcompute.Toolkit.Comparers;

/// <summary>
/// A comparer that casts the source <typeparamref name="TSource"/> arguments to <typeparamref name="TElement"/> before comparing them.
/// </summary>
public class CastingComparer<TSource, TElement> : Comparer<TSource>
    where TElement : TSource
{
    private readonly IComparer<TElement> _comparer;

    internal CastingComparer(IComparer<TElement>? comparer = null)
    {
        _comparer = comparer ?? Comparer<TElement>.Default;
    }

    public override int Compare(TSource? x, TSource? y)
    {
        var xCast = (TElement)x!;
        var yCast = (TElement)y!;

        return _comparer.Compare(xCast, yCast);
    }

    /// <summary>
    /// Gets the default <see cref="CastingComparer{TSource, TElement}"/> that does not delegate to another comparer.
    /// </summary>
    public new static CastingComparer<TSource, TElement> Default { get; } = new CastingComparer<TSource, TElement>(Comparer<TElement>.Default);
}

/// <summary>
/// Organizes the various ways to create a <see cref="CastingComparer{TSource, TElement}"/>.
/// </summary>
public static class CastingComparer
{
    /// <summary>
    /// Creates a new <see cref="CastingComparer{TSource, TElement}"/>.
    /// </summary>
    public static CastingComparer<TSource, TElement> Create<TSource, TElement>(IComparer<TElement>? comparer = null)
        where TElement : TSource
    {
        return new(comparer);
    }

    /// <summary>
    /// Creates a generic <see cref="CastingComparer{TSource, TElement}"/> based on the provided generic source type and non-generic element type.
    /// </summary>
    public static Comparer<TSource> Create<TSource>(Type elementType, IComparer? comparer = null)
    {
        Guard.IsNotNull(elementType);

        var comparerType = typeof(CastingComparer<,>).MakeGenericType(typeof(TSource), elementType);

        return (Comparer<TSource>)Activator.CreateInstance(comparerType, BindingFlags.NonPublic, null, new object?[] { comparer }, null)!;
    }

    /// <summary>
    /// Creates a generic <see cref="CastingComparer{TSource, TElement}"/> based on the provided non-generic source and element types.
    /// </summary>
    public static IComparer Create(Type sourceType, Type elementType, IComparer? comparer = null)
    {
        Guard.IsNotNull(sourceType);
        Guard.IsNotNull(elementType);

        var comparerType = typeof(CastingComparer<,>).MakeGenericType(sourceType, elementType);

        return (IComparer)Activator.CreateInstance(comparerType, BindingFlags.NonPublic, null, new object?[] { comparer }, null)!;
    }
}