using System.Reflection;

namespace Outcompute.Toolkit.Comparers;

/// <summary>
/// A comparer that compares two <typeparamref name="TSource"/> values via the specified selector.
/// </summary>
public class SelectComparer<TSource, TResult> : Comparer<TSource>
{
    private readonly Func<TSource?, TResult?> _selector;
    private readonly IComparer<TResult> _comparer;

    internal SelectComparer(Func<TSource?, TResult?> selector, IComparer<TResult>? comparer = null)
    {
        Guard.IsNotNull(selector);

        _selector = selector;
        _comparer = comparer ?? Comparer<TResult>.Default;
    }

    public override int Compare(TSource? x, TSource? y)
    {
        var xValue = _selector(x);
        var yValue = _selector(y);

        return _comparer.Compare(xValue, yValue);
    }
}

/// <summary>
/// Organizes the various ways to create a <see cref="SelectComparer{TSource, TResult}"/>.
/// </summary>
public static class SelectComparer
{
    /// <summary>
    /// Creates a new <see cref="SelectComparer{TSource, TResult}"/>.
    /// </summary>
    public static SelectComparer<TSource, TResult> Create<TSource, TResult>(Func<TSource?, TResult?> selector, IComparer<TResult>? comparer = null)
    {
        return new(selector, comparer);
    }

    /// <summary>
    /// Creates a new <see cref="SelectComparer{TSource, TResult}"/> that compares <typeparamref name="TSource"/> items via the specified property of type <typeparamref name="TResult"/>.
    /// </summary>
    public static SelectComparer<TSource, TResult> Create<TSource, TResult>(string propertyOrFieldName, IComparer<TResult>? comparer = null)
    {
        Guard.IsNotNull(propertyOrFieldName);

        // create the selector from the property name
        var item = Expression.Parameter(typeof(TSource));
        var getter = Expression.PropertyOrField(item, propertyOrFieldName);
        var selector = Expression.Lambda<Func<TSource?, TResult?>>(getter, item).Compile();

        // defer to the generic overload
        return Create(selector, comparer);
    }

    /// <summary>
    /// Creates a new <see cref="SelectComparer{TSource, TResult}"/> that compares <typeparamref name="TSource"/> items via the specified property of unknown type.
    /// </summary>
    public static Comparer<TSource> Create<TSource>(string propertyOrFieldName, IComparer? comparer = null)
    {
        Guard.IsNotNull(propertyOrFieldName);

        // create the selector from the property name
        var item = Expression.Parameter(typeof(TSource));
        var getter = Expression.PropertyOrField(item, propertyOrFieldName);
        var delegateType = Expression.GetFuncType(typeof(TSource), getter.Type);
        var selector = Expression.Lambda(delegateType, getter, item).Compile();

        // create the comparer with discovered arguments
        var comparerType = typeof(SelectComparer<,>).MakeGenericType(typeof(TSource), getter.Type);
        return (Comparer<TSource>)Activator.CreateInstance(comparerType, BindingFlags.NonPublic, null, new object?[] { selector, comparer }, null)!;
    }
}