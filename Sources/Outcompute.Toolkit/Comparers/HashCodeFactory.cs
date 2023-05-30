using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Comparers;

/// <summary>
/// A factory that calculates hash codes based on an arbitrary collection of crieteria.
/// </summary>
/// <remarks>
/// This class is a building block for complex late-bound comparers.
/// </remarks>
public class HashCodeFactory<TSource>
{
    private readonly List<HashCodeContributor<TSource>> _contributors = new();

    /// <summary>
    /// Creates a new <see cref="HashCodeFactory{TSource}"/> without any steps.
    /// </summary>
    public HashCodeFactory()
    {
    }

    /// <summary>
    /// Creates a new <see cref="HashCodeFactory{TSource}"/> based on the specified <see cref="WireExpression"/> sequence.
    /// </summary>
    public HashCodeFactory(IEnumerable<WireExpression> expressions)
    {
        Guard.IsNotNull(expressions);

        foreach (var expression in expressions)
        {
            Add(expression);
        }
    }

    /// <summary>
    /// Adds a new hash code calculation step based on the specified <see cref="WireExpression"/>.
    /// </summary>
    public void Add(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        var contributor = HashCodeContributor.Create<TSource>(expression);

        _contributors.Add(contributor);
    }

    /// <summary>
    /// Adds the specified <see cref="HashCodeContributor{TSource}"/> to the calculation steps.
    /// </summary>
    public void Add(HashCodeContributor<TSource> contributor)
    {
        Guard.IsNotNull(contributor);

        _contributors.Add(contributor);
    }

    /// <summary>
    /// Calculates the hash code for the specified <typeparamref name="TSource"/> item.
    /// </summary>
    public int GetHashCode(TSource source)
    {
        Guard.IsNotNull(source);

        var builder = new HashCode();

        foreach (var item in _contributors)
        {
            item.Contribute(source, builder);
        }

        return builder.ToHashCode();
    }
}

/// <summary>
/// Quality of life extensions for <see cref="HashCodeFactory{TSource}"/>.
/// </summary>
public static class HashCodeFactoryEnumerableExtensions
{
    /// <summary>
    /// Creates a new <see cref="HashCodeFactory{TSource}"/> based on the specified <see cref="WireExpression"/> sequence.
    /// </summary>
    public static HashCodeFactory<TSource> ToHashCodeFactory<TSource>(this IEnumerable<WireExpression> expressions)
    {
        Guard.IsNotNull(expressions);

        return new HashCodeFactory<TSource>(expressions);
    }
}

/// <summary>
/// A class that knows how to contribute to a hash code builder in a certain way.
/// </summary>
/// <remarks>
/// This class is a building block for <see cref="HashCodeFactory{TSource}"/>.
/// </remarks>
public abstract class HashCodeContributor<TSource>
{
    /// <summary>
    /// Adds a value to specified hash code builder from the specified source.
    /// </summary>
    public abstract void Contribute(TSource source, HashCode builder);
}

/// <summary>
/// Implements a class that contributes to a hash code based on the specified selector.
/// </summary>
/// <remarks>
/// This class is a building block for <see cref="HashCodeFactory{TSource}"/>.
/// </remarks>
public class HashCodeContributor<TSource, TResult> : HashCodeContributor<TSource>
{
    private readonly Func<TSource, TResult> _selector;

    internal HashCodeContributor(Func<TSource, TResult> selector)
    {
        Guard.IsNotNull(selector);

        _selector = selector;
    }

    public override void Contribute(TSource source, HashCode builder)
    {
        Guard.IsNotNull(source);

        var value = _selector(source);

        builder.Add(value);
    }
}

/// <summary>
/// Organizes the various ways to create a <see cref="HashCodeContributor{TSource, TResult}"/>.
/// </summary>
public static class HashCodeContributor
{
    /// <summary>
    /// Creates a new <see cref="HashCodeContributor{TSource, TResult}"/>.
    /// </summary>
    public static HashCodeContributor<TSource, TResult> Create<TSource, TResult>(Func<TSource, TResult> selector)
    {
        return new(selector);
    }

    /// <summary>
    /// Creates a new <see cref="HashCodeContributor{TSource, TResult}"/> based on the specified <see cref="WireExpression"/>.
    /// </summary>
    public static HashCodeContributor<TSource> Create<TSource>(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        // convert the query expression to a linq expression so we can identify the result type
        var (result, item) = expression.ToLinqExpression<TSource>();

        // define the selector function type from the discovered result type
        var type = Expression.GetFuncType(typeof(TSource), result.Type);

        // compile the selector function
        var func = Expression.Lambda(type, result, item).Compile();

        // create the selector based on the compiled func
        return (HashCodeContributor<TSource>)((Func<Func<object, object>, HashCodeContributor<object, object>>)Create<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod()
            .Invoke(null, new object[] { func })!;
    }
}