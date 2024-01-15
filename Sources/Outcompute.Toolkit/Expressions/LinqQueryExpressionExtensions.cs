using Outcompute.Toolkit.Collections;
using Outcompute.Toolkit.Comparers;
using Outcompute.Toolkit.Expressions.Visitors;
using System.Reflection;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Enables integration of <see cref="WireExpression"/> trees with LINQ-based user code.
/// </summary>
public static class LinqQueryExpressionExtensions
{
    #region Shared

    /// <summary>
    /// Converts the specified <see cref="WireExpression"/> to a LINQ <see cref="Expression"/> targetting <typeparamref name="TSource"/>.
    /// </summary>
    public static (Expression Result, ParameterExpression Item) ToLinqExpression<TSource>(this WireExpression expression)
    {
        Guard.IsNotNull(expression);

        return LinqWireExpressionVisitor.Visit<TSource>(expression);
    }

    /// <summary>
    /// Converts the specified <see cref="WireExpression"/> to a LINQ <see cref="Expression"/> targetting <typeparamref name="TSource"/>
    /// using the specified LINQ <see cref="ParameterExpression"/> as the item placeholder
    /// </summary>
    public static Expression ToLinqExpression<TSource>(this WireExpression expression, ParameterExpression item)
    {
        Guard.IsNotNull(expression);
        Guard.IsNotNull(item);

        return LinqWireExpressionVisitor.Visit<TSource>(expression, item);
    }

    /// <summary>
    /// Converts the specified <see cref="WireExpression"/> to a LINQ <see cref="LambdaExpression"/> targetting <typeparamref name="TSource"/>.
    /// </summary>
    public static LambdaExpression ToLambdaExpression<TSource>(this WireExpression expression)
    {
        Guard.IsNotNull(expression);

        var (result, item) = expression.ToLinqExpression<TSource>();

        return Expression.Lambda(result, item);
    }

    /// <summary>
    /// Compiles the specified <see cref="WireExpression"/> to an invocable <see cref="Func{TSource, TResult}"/>.
    /// </summary>
    public static Func<TSource, TResult> ToDelegate<TSource, TResult>(this WireExpression expression)
    {
        Guard.IsNotNull(expression);

        var (result, item) = expression.ToLinqExpression<TSource>();

        return Expression.Lambda<Func<TSource, TResult>>(result, item).Compile();
    }

    /// <summary>
    /// Compiles the specified <see cref="WireExpression"/> to an invocable late-bound <see cref="Delegate"/>.
    /// </summary>
    public static Delegate ToDelegate<TSource>(this WireExpression expression, Type delegateType)
    {
        Guard.IsNotNull(expression);
        Guard.IsNotNull(delegateType);

        var (result, item) = expression.ToLinqExpression<TSource>();

        return Expression.Lambda(delegateType, result, item).Compile();
    }

    /// <summary>
    /// Compiles the specified <see cref="WireExpression"/> to an invocable <see cref="Func{TSource, bool}"/>.
    /// </summary>
    public static Func<TSource, bool> ToPredicate<TSource>(this WireExpression expression)
    {
        Guard.IsNotNull(expression);

        var (result, item) = expression.ToLinqExpression<TSource>();

        return Expression.Lambda<Func<TSource, bool>>(result, item).Compile();
    }

    #endregion Shared

    #region Where - Sequential

    /// <summary>
    /// Filters a sequence of values based on a predicate <see cref="WireExpression"/> that is compiled against <typeparamref name="TSource"/>.
    /// </summary>
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, WireExpression expression)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(expression);

        var predicate = expression.ToPredicate<TSource>();

        return source.Where(predicate);
    }

    /// <summary>
    /// Filters a sequence of <typeparamref name="TSource"/> values based on a predicate <see cref="WireExpression"/> that is compiled against <typeparamref name="TElement"/>.
    /// </summary>
    public static IEnumerable<TSource> Where<TSource, TElement>(this IEnumerable<TSource> source, WireExpression expression)
        where TElement : TSource
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(expression);

        var predicate = expression.ToPredicate<TElement>();

        return source.Cast<TElement>().Where(predicate).Cast<TSource>();
    }

    /// <summary>
    /// Caches constructed delegates for <see cref="Where{TSource, TElement}(IEnumerable{TSource}, WireExpression)"/>.
    /// </summary>
    private static readonly ConcurrentLazyLookup<(Type SourceType, Type ElementType), Delegate> WhereSequentialDelegates = new(((Type SourceType, Type ElementType) args) =>
    {
        var enumerableType = typeof(IEnumerable<>).MakeGenericType(args.SourceType);

        var delegateType = Expression.GetFuncType(enumerableType, typeof(WireExpression), enumerableType);

        return ((Func<IEnumerable<object>, WireExpression, IEnumerable<object>>)Where<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(args.SourceType, args.ElementType)
            .CreateDelegate(delegateType);
    });

    /// <summary>
    /// Filters a sequence of <typeparamref name="TSource"/> values based on a predicate <see cref="WireExpression"/> that is compiled against <paramref name="elementType"/>.
    /// </summary>
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, WireExpression expression, Type elementType)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(expression);
        Guard.IsNotNull(elementType);

        return (IEnumerable<TSource>)WhereSequentialDelegates[(typeof(TSource), elementType)].DynamicInvoke(source, expression)!;
    }

    #endregion Where - Sequential

    #region Where - Parallel

    /// <summary>
    /// Filters in parallel a sequence of values based on a predicate <see cref="WireExpression"/> that is compiled against <typeparamref name="TSource"/>.
    /// </summary>
    public static ParallelQuery<TSource> Where<TSource>(this ParallelQuery<TSource> source, WireExpression expression)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(expression);

        var predicate = expression.ToPredicate<TSource>();

        return source.Where(predicate);
    }

    /// <summary>
    /// Filters in parallel a sequence of <typeparamref name="TSource"/> values based on a predicate <see cref="WireExpression"/> that is compiled against <typeparamref name="TElement"/>.
    /// </summary>
    public static ParallelQuery<TSource> Where<TSource, TElement>(this ParallelQuery<TSource> source, WireExpression expression)
        where TElement : TSource
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(expression);

        var predicate = expression.ToPredicate<TElement>();

        return source.Cast<TElement>().Where(predicate).Cast<TSource>();
    }

    /// <summary>
    /// Caches constructed delegates for <see cref="Where{TSource, TElement}(ParallelQuery{TSource}, WireExpression)"/>.
    /// </summary>
    private static readonly ConcurrentLazyLookup<(Type SourceType, Type ElementType), Delegate> WhereParallelDelegates = new(((Type SourceType, Type ElementType) args) =>
    {
        var enumerableType = typeof(ParallelQuery<>).MakeGenericType(args.SourceType);

        var delegateType = Expression.GetFuncType(enumerableType, typeof(WireExpression), enumerableType);

        return ((Func<ParallelQuery<object>, WireExpression, ParallelQuery<object>>)Where<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(args.SourceType, args.ElementType)
            .CreateDelegate(delegateType);
    });

    /// <summary>
    /// Filters a sequence of <typeparamref name="TSource"/> values based on a predicate <see cref="WireExpression"/> that is compiled against <paramref name="elementType"/>.
    /// </summary>
    public static ParallelQuery<TSource> Where<TSource>(this ParallelQuery<TSource> source, WireExpression expression, Type elementType)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(expression);
        Guard.IsNotNull(elementType);

        return (ParallelQuery<TSource>)WhereParallelDelegates[(typeof(TSource), elementType)].DynamicInvoke(source, expression)!;
    }

    #endregion Where - Parallel

    #region Where - Async

    /// <summary>
    /// Filters an async-sequence of values based on a predicate <see cref="WireExpression"/> that is compiled against <typeparamref name="TSource"/>.
    /// </summary>
    public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, WireExpression expression)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(expression);

        var predicate = expression.ToPredicate<TSource>();

        return source.Where(predicate);
    }

    /// <summary>
    /// Filters an async-sequence of values based on a predicate <see cref="WireExpression"/> that is compiled against <typeparamref name="TElement"/>.
    /// </summary>
    public static IAsyncEnumerable<TSource> Where<TSource, TElement>(this IAsyncEnumerable<TSource> source, WireExpression expression)
        where TElement : TSource
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(expression);

        var predicate = expression.ToPredicate<TElement>();

        return source.Select(x => (TElement)x!).Where(predicate).Select(x => (TSource)x);
    }

    /// <summary>
    /// Caches constructed delegates for <see cref="Where{TSource, TElement}(IAsyncEnumerable{TSource}, WireExpression)"/>.
    /// </summary>
    private static readonly ConcurrentLazyLookup<(Type SourceType, Type ElementType), Delegate> WhereAsyncDelegates = new(((Type SourceType, Type ElementType) args) =>
    {
        var enumerableType = typeof(ParallelQuery<>).MakeGenericType(args.SourceType);

        var delegateType = Expression.GetFuncType(enumerableType, typeof(WireExpression), enumerableType);

        return ((Func<ParallelQuery<object>, WireExpression, ParallelQuery<object>>)Where<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(args.SourceType, args.ElementType)
            .CreateDelegate(delegateType);
    });

    /// <summary>
    /// Filters an async-sequence of <typeparamref name="TSource"/> values based on a predicate <see cref="WireExpression"/> that is compiled against <paramref name="elementType"/>.
    /// </summary>
    public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, WireExpression expression, Type elementType)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(expression);
        Guard.IsNotNull(elementType);

        return (IAsyncEnumerable<TSource>)WhereAsyncDelegates[(typeof(TSource), elementType)].DynamicInvoke(source, expression)!;
    }

    #endregion Where - Async

    #region Select - Shared

    /// <summary>
    /// Compiles an efficient property or field selector for the specified named property of type <typeparamref name="TProperty"/> against the specified <typeparamref name="TSource"/> type.
    /// </summary>
    private static Func<TSource, TProperty> CreatePropertyOrFieldSelector<TSource, TProperty>(string property)
    {
        var item = Expression.Parameter(typeof(TSource));
        var getter = Expression.PropertyOrField(item, property);
        var lambda = Expression.Lambda<Func<TSource, TProperty>>(getter, item);

        return lambda.Compile();
    }

    #endregion Select - Shared

    #region Select - Sequential

    /// <summary>
    /// Projects the specified property or field by name.
    /// </summary>
    public static IEnumerable<TProperty> Select<TSource, TProperty>(this IEnumerable<TSource> source, string property)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(property);

        var selector = CreatePropertyOrFieldSelector<TSource, TProperty>(property);

        return source.Select(selector);
    }

    #endregion Select - Sequential

    #region Select - Parallel

    /// <summary>
    /// Projects in parallel the specified property or field by name.
    /// </summary>
    public static ParallelQuery<TProperty> Select<TSource, TProperty>(this ParallelQuery<TSource> source, string property)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(property);

        var selector = CreatePropertyOrFieldSelector<TSource, TProperty>(property);

        return source.Select(selector);
    }

    #endregion Select - Parallel

    #region Select - Async

    /// <summary>
    /// Asynchronously projects the specified property or field by name.
    /// </summary>
    public static IAsyncEnumerable<TProperty> Select<TSource, TProperty>(this IAsyncEnumerable<TSource> source, string property)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(property);

        var selector = CreatePropertyOrFieldSelector<TSource, TProperty>(property);

        return source.Select(selector);
    }

    #endregion Select - Async

    #region OrderBy - Shared

    /// <summary>
    /// Roots comparer information to amortize reflection cost.
    /// </summary>
    private static readonly ConcurrentLazyLookup<Type, (object ComparerInstance, MethodInfo ComparerMethod)> ComparerInfo = new(type =>
    {
        var comparerType = typeof(Comparer<>).MakeGenericType(type);
        var comparerInstance = comparerType.GetProperty("Default")!.GetValue(null)!;
        var comparerMethod = comparerType.GetMethod("Compare")!;

        return (comparerInstance, comparerMethod);
    });

    /// <summary>
    /// Roots type information to amortize reflection.
    /// </summary>
    private static class OrderBySharedLookup<TSource>
    {
        /// <summary>
        /// Roots late bound calls to <see cref="ToComparer{TSource, TElement}(OrderByCriteria)"/> to amortize reflection.
        /// </summary>
        public static readonly ConcurrentLazyLookup<Type, Func<OrderByCriteria, Comparer<TSource>>> ToComparerMethods = new(elementType =>
        {
            // discover the correct generic method to call
            var method = ((Func<OrderByCriteria, Comparer<object>>)ToComparer<object, object>)
                .Method
                .GetGenericMethodDefinition()
                .MakeGenericMethod(typeof(TSource), elementType);

            // generate a typed lambda to call the method
            var parameter = Expression.Parameter(typeof(OrderByCriteria), "criteria");
            var call = Expression.Call(method, parameter);
            var lambda = Expression.Lambda<Func<OrderByCriteria, Comparer<TSource>>>(call, parameter);

            return lambda.Compile();
        });
    }

    /// <summary>
    /// Generates an expression that works as below:
    /// <code>
    /// var leftValue = LeftSelector(leftItem);
    /// var rightValue = RightSelector(rightItem);
    /// var result = Comparer&lt;TSource&gt;.Default.Compare(leftValue, rightValue);
    /// return direction == ascending ? result : -result;
    /// </code>
    /// </summary>
    /// <remarks>
    /// This expression is a fragment designed to be used in a larged expression.
    /// </remarks>
    public static Expression ToComparisonExpression<TSource>(this OrderByCriterion criterion, ParameterExpression left, ParameterExpression right)
    {
        // generate a selector expression for the left item
        var leftSelector = criterion.Expression.ToLinqExpression<TSource>(left);

        // generate a selector expression for the right item
        var rightSelector = criterion.Expression.ToLinqExpression<TSource>(right);

        // the selected property types must match
        if (leftSelector.Type != rightSelector.Type)
        {
            throw new ArgumentException($"The left item property type '{leftSelector.Type.FullName}' and right item property type '{rightSelector.Type.FullName}' must match");
        }

        // generate a call to appropriate comparer method for the accessed property
        var (comparerInstance, comparerMethod) = ComparerInfo[leftSelector.Type];
        var compare = Expression.Call(Expression.Constant(comparerInstance), comparerMethod, leftSelector, rightSelector);

        // adjust the comparison as per the ordering criteria
        return criterion.Direction == OrderByDirection.Ascending ? compare : Expression.Negate(compare);
    }

    /// <summary>
    /// Compiles a <see cref="Comparison{T}"/> for the specified <paramref name="criterion"/>.
    /// </summary>
    public static Comparison<TSource> ToComparison<TSource>(this OrderByCriterion criterion)
    {
        Guard.IsNotNull(criterion);

        var left = Expression.Parameter(typeof(TSource), "left");
        var right = Expression.Parameter(typeof(TSource), "right");
        var expression = criterion.ToComparisonExpression<TSource>(left, right);
        var lambda = Expression.Lambda<Comparison<TSource>>(expression, left, right);

        return lambda.Compile();
    }

    /// <summary>
    /// Compiles a <see cref="Comparison{T}"/> for the specified <paramref name="criteria"/>.
    /// </summary>
    public static Comparison<TSource> ToComparison<TSource>(this OrderByCriteria criteria)
    {
        Guard.IsNotNull(criteria);

        if (criteria.Criterions.Length == 0)
        {
            throw new ArgumentException($"Argument '{nameof(criteria)}' must not be empty", nameof(criteria));
        }

        // pin to avoid redundancy
        var zero = Expression.Constant(0, typeof(int));

        // represents the left parameter of the generated comparison lambda
        var left = Expression.Parameter(typeof(TSource), "left");

        // represents the right parameter of the generated comparison lambda
        var right = Expression.Parameter(typeof(TSource), "right");

        // represents the return label of the comparison code
        var target = Expression.Label(typeof(int));

        // generate shortcut decision code for each criterion
        var statements = new List<Expression>();
        foreach (var criterion in criteria.Criterions)
        {
            // generate a comparison expression for the current criterion that takes the shared left/right parameters
            var comparison = criterion.ToComparisonExpression<TSource>(left, right);

            // generate a declaration for a temp variable
            var temp = Expression.Variable(typeof(int), "temp");

            // generate the assignment of the comparison to the temp variable
            var assign = Expression.Assign(temp, comparison);

            // generate the shortcut decision
            var decision = Expression.IfThen(Expression.NotEqual(temp, zero), Expression.Return(target, temp));

            // generate a scope block that contains the above steps
            var block = Expression.Block(new[] { temp }, new Expression[] { assign, decision });
            statements.Add(block);
        }

        // return zero by default
        statements.Add(Expression.Label(target, zero));

        // we can now compile
        return Expression.Lambda<Comparison<TSource>>(Expression.Block(statements), left, right).Compile();
    }

    /// <summary>
    /// Compiles a <see cref="Comparer{T}"/> for the specified <paramref name="criteria"/>.
    /// </summary>
    public static Comparer<TSource> ToComparer<TSource>(this OrderByCriteria criteria)
    {
        Guard.IsNotNull(criteria);

        var comparison = criteria.ToComparison<TSource>();

        return Comparer<TSource>.Create(comparison);
    }

    /// <summary>
    /// Compiles a <see cref="Comparer{T}"/> for the specified <paramref name="criteria"/> against <typeparamref name="TElement"/>.
    /// </summary>
    public static Comparer<TSource> ToComparer<TSource, TElement>(this OrderByCriteria criteria)
        where TElement : TSource
    {
        Guard.IsNotNull(criteria);

        var comparison = criteria.ToComparison<TElement>();

        return Comparer<TSource>.Create((x, y) => comparison((TElement)x!, (TElement)y!));
    }

    /// <summary>
    /// Compiles a <see cref="Comparer{T}"/> for the specified <paramref name="criteria"/> against <paramref name="elementType"/>.
    /// </summary>
    public static Comparer<TSource> ToComparer<TSource>(this OrderByCriteria criteria, Type elementType)
    {
        Guard.IsNotNull(criteria);
        Guard.IsNotNull(elementType);

        return OrderBySharedLookup<TSource>.ToComparerMethods[elementType](criteria);
    }

    #endregion OrderBy - Shared

    #region OrderBy - Sequential

    /// <summary>
    /// Sorts the specified sequence by using the specified criteria.
    /// </summary>
    public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, OrderByCriteria criteria)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TSource>();

        return source.OrderBy(x => x, comparer);
    }

    /// <summary>
    /// Sorts the specified sequence by using the specified criteria targetting <typeparamref name="TElement"/>.
    /// </summary>
    public static IOrderedEnumerable<TSource> OrderBy<TSource, TElement>(this IEnumerable<TSource> source, OrderByCriteria criteria)
        where TElement : TSource
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TElement>();

        return source.OrderBy(x => (TElement)x!, comparer);
    }

    /// <summary>
    /// Sorts the specified sequence by using the specified criteria targetting <paramref name="elementType"/>.
    /// </summary>
    public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, OrderByCriteria criteria, Type elementType)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);
        Guard.IsNotNull(elementType);

        var method = ((Func<IEnumerable<object>, OrderByCriteria, IOrderedEnumerable<object>>)OrderBy<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(typeof(TSource), elementType);

        return (IOrderedEnumerable<TSource>)method.Invoke(null, new object[] { source, criteria })!;
    }

    #endregion OrderBy - Sequential

    #region OrderBy - Parallel

    /// <summary>
    /// Sorts in parallel the specified sequence by using the specified criteria.
    /// </summary>
    public static OrderedParallelQuery<TSource> OrderBy<TSource>(this ParallelQuery<TSource> source, OrderByCriteria criteria)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TSource>();

        return source.OrderBy(x => x, comparer);
    }

    /// <summary>
    /// Sorts in parallel the specified sequence by using the specified criteria targetting <typeparamref name="TElement"/>.
    /// </summary>
    public static OrderedParallelQuery<TSource> OrderBy<TSource, TElement>(this ParallelQuery<TSource> source, OrderByCriteria criteria)
        where TElement : TSource
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TElement>();

        return source.OrderBy(x => (TElement)x!, comparer);
    }

    /// <summary>
    /// Sorts the specified sequence by using the specified criteria targetting <paramref name="elementType"/>.
    /// </summary>
    public static OrderedParallelQuery<TSource> OrderBy<TSource>(this ParallelQuery<TSource> source, OrderByCriteria criteria, Type elementType)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);
        Guard.IsNotNull(elementType);

        var method = ((Func<ParallelQuery<object>, OrderByCriteria, OrderedParallelQuery<object>>)OrderBy<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(typeof(TSource), elementType);

        return (OrderedParallelQuery<TSource>)method.Invoke(null, new object[] { source, criteria })!;
    }

    #endregion OrderBy - Parallel

    #region OrderBy - Async

    /// <summary>
    /// Sorts the specified async sequence by using the specified criteria.
    /// </summary>
    public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource>(this IAsyncEnumerable<TSource> source, OrderByCriteria criteria)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TSource>();

        return source.OrderBy(x => x, comparer);
    }

    /// <summary>
    /// Sorts the specified async sequence by using the specified criteria targetting <typeparamref name="TElement"/>.
    /// </summary>
    public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TElement>(this IAsyncEnumerable<TSource> source, OrderByCriteria criteria)
        where TElement : TSource
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TElement>();

        return source.OrderBy(x => (TElement)x!, comparer);
    }

    /// <summary>
    /// Sorts specified async sequence by using the specified criteria targetting <paramref name="elementType"/>.
    /// </summary>
    public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource>(this IAsyncEnumerable<TSource> source, OrderByCriteria criteria, Type elementType)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);
        Guard.IsNotNull(elementType);

        var method = ((Func<IAsyncEnumerable<object>, OrderByCriteria, IOrderedAsyncEnumerable<object>>)OrderBy<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(typeof(TSource), elementType);

        return (IOrderedAsyncEnumerable<TSource>)method.Invoke(null, new object[] { source, criteria })!;
    }

    #endregion OrderBy - Async

    #region ThenBy - Sequential

    /// <summary>
    /// Performs a subsequent ordering of the elements in a sequence by using the specified criteria.
    /// </summary>
    public static IOrderedEnumerable<TSource> ThenBy<TSource>(this IOrderedEnumerable<TSource> source, OrderByCriteria criteria)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TSource>();

        return source.ThenBy(x => x, comparer);
    }

    /// <summary>
    /// Performs a subsequent ordering of the elements in a sequence by using the specified criteria targetting <typeparamref name="TElement"/>.
    /// </summary>
    public static IOrderedEnumerable<TSource> ThenBy<TSource, TElement>(this IOrderedEnumerable<TSource> source, OrderByCriteria criteria)
        where TElement : TSource
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TElement>();

        return source.ThenBy(x => (TElement)x!, comparer);
    }

    /// <summary>
    /// Performs a subsequent ordering of the elements in a sequence by using the specified criteria targetting <paramref name="elementType"/>.
    /// </summary>
    public static IOrderedEnumerable<TSource> ThenBy<TSource>(this IOrderedEnumerable<TSource> source, OrderByCriteria criteria, Type elementType)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);
        Guard.IsNotNull(elementType);

        var method = ((Func<IOrderedEnumerable<object>, OrderByCriteria, IOrderedEnumerable<object>>)ThenBy<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(typeof(TSource), elementType);

        return (IOrderedEnumerable<TSource>)method.Invoke(null, new object[] { source, criteria })!;
    }

    #endregion ThenBy - Sequential

    #region ThenBy - Parallel

    /// <summary>
    /// Performs in parallel a subsequent ordering of the elements in a sequence by using the specified criteria.
    /// </summary>
    public static OrderedParallelQuery<TSource> ThenBy<TSource>(this OrderedParallelQuery<TSource> source, OrderByCriteria criteria)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TSource>();

        return source.ThenBy(x => x, comparer);
    }

    /// <summary>
    /// Performs in paralle a subsequent ordering of the elements in a sequence by using the specified criteria targetting <typeparamref name="TElement"/>.
    /// </summary>
    public static OrderedParallelQuery<TSource> ThenBy<TSource, TElement>(this OrderedParallelQuery<TSource> source, OrderByCriteria criteria)
        where TElement : TSource
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TElement>();

        return source.ThenBy(x => (TElement)x!, comparer);
    }

    /// <summary>
    /// Performs in parallel a subsequent ordering of the elements in a sequence by using the specified criteria targetting <paramref name="elementType"/>.
    /// </summary>
    public static OrderedParallelQuery<TSource> ThenBy<TSource>(this OrderedParallelQuery<TSource> source, OrderByCriteria criteria, Type elementType)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);
        Guard.IsNotNull(elementType);

        var method = ((Func<OrderedParallelQuery<object>, OrderByCriteria, OrderedParallelQuery<object>>)ThenBy<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(typeof(TSource), elementType);

        return (OrderedParallelQuery<TSource>)method.Invoke(null, new object[] { source, criteria })!;
    }

    #endregion ThenBy - Parallel

    #region ThenBy - Async

    /// <summary>
    /// Performs a subsequent ordering of the elements in an async sequence by using the specified criteria.
    /// </summary>
    public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource>(this IOrderedAsyncEnumerable<TSource> source, OrderByCriteria criteria)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TSource>();

        return source.ThenBy(x => x, comparer);
    }

    /// <summary>
    /// Performs a subsequent ordering of the elements in an async sequence by using the specified criteria targetting <typeparamref name="TElement"/>.
    /// </summary>
    public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TElement>(this IOrderedAsyncEnumerable<TSource> source, OrderByCriteria criteria)
        where TElement : TSource
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);

        var comparer = criteria.ToComparer<TElement>();

        return source.ThenBy(x => (TElement)x!, comparer);
    }

    /// <summary>
    /// Performs a subsequent ordering of the elements in an async sequence by using the specified criteria targetting <paramref name="elementType"/>.
    /// </summary>
    public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource>(this IOrderedAsyncEnumerable<TSource> source, OrderByCriteria criteria, Type elementType)
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(criteria);
        Guard.IsNotNull(elementType);

        var method = ((Func<IOrderedAsyncEnumerable<object>, OrderByCriteria, IOrderedAsyncEnumerable<object>>)ThenBy<object, object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(typeof(TSource), elementType);

        return (IOrderedAsyncEnumerable<TSource>)method.Invoke(null, new object[] { source, criteria })!;
    }

    #endregion ThenBy - Async

    #region GroupBy - Shared

    /// <summary>
    /// Compiles a composite hash code calculation delegate from the specified set of expressions.
    /// </summary>
    /// <remarks>
    /// If there are no expressions to evaluate then the hashcode returned is a constant.
    /// </remarks>
    internal static HashCodeCalculation<TSource> ToHashCodeCalculation<TSource>(this IEnumerable<WireExpression> expressions)
    {
        Guard.IsNotNull(expressions);

        return expressions.ToHashCodeFactory<TSource>().GetHashCode;
    }

    /// <summary>
    /// Compiles a composite hash code calculation delegate from the set of expressions in the specified criteria.
    /// </summary>
    /// <remarks>
    /// If there are no expressions to evaluate then the hashcode returned will always be the same.
    /// </remarks>
    internal static HashCodeCalculation<TSource> ToHashCodeCalculation<TSource>(this GroupByCriteria criteria)
    {
        Guard.IsNotNull(criteria);

        return criteria.Expressions.ToHashCodeCalculation<TSource>();
    }

    #endregion GroupBy - Shared
}