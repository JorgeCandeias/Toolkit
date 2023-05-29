using System.Reflection;

namespace Outcompute.Toolkit.Expressions.Visitors;

/// <summary>
/// Implements a <see cref="QueryExpressionVisitor"/> that transforms <see cref="QueryExpression"/> trees into LINQ <see cref="Expression"/> trees.
/// </summary>
internal sealed class LinqQueryExpressionVisitor<T> : QueryExpressionVisitor
{
    /// <summary>
    /// Holds the current expression stack as it is being processed.
    /// </summary>
    private readonly Stack<Expression> _stack = new();

    /// <summary>
    /// Gets the transformed LINQ <see cref="Expression"/> tree.
    /// </summary>
    public Expression Result => _stack.Peek();

    /// <summary>
    /// Gets the enumeration parameter used with a linq expression enumerable lambda.
    /// </summary>
    public ParameterExpression Item { get; } = Expression.Parameter(typeof(T), "item");

    /// <summary>
    /// Converts the target <see cref="QueryExpression"/> to a LINQ <see cref="Expression"/>.
    /// </summary>
    private Expression Convert(QueryExpression expression)
    {
        // visit the expression
        // this will push the converted expression to the stack
        Visit(expression);

        // pop and return the converted expression from the stack
        return _stack.Pop();
    }

    protected internal override QueryExpression VisitDefault(DefaultExpression expression)
    {
        _stack.Push(Expression.Empty());

        return expression;
    }

    protected internal override QueryExpression VisitItem(ItemExpression expression)
    {
        _stack.Push(Item);

        return expression;
    }

    protected internal override QueryExpression VisitProperty(PropertyExpression expression)
    {
        var name = expression.Name;
        var target = expression.Target is ItemExpression ? Item : Convert(expression.Target);
        var converted = Expression.Property(target, name);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitField(FieldExpression expression)
    {
        var name = expression.Name;
        var target = expression.Target is ItemExpression ? Item : Convert(expression.Target);
        var converted = Expression.Field(target, name);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitPropertyOrField(PropertyOrFieldExpression expression)
    {
        var name = expression.Name;
        var target = expression.Target is ItemExpression ? Item : Convert(expression.Target);
        var converted = Expression.PropertyOrField(target, name);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitNot(NotExpression expression)
    {
        var target = Convert(expression.Target);
        var converted = Expression.Not(target);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitIsNull(IsNullExpression expression)
    {
        var target = Convert(expression.Target);

        // if the type does not support null then shortcut to false
        Expression converted =
            target.Type.IsValueType && Nullable.GetUnderlyingType(target.Type) is null
            ? Expression.Constant(false, typeof(bool))
            : Expression.Equal(target, Expression.Constant(null, target.Type));

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitIsNotNull(IsNotNullExpression expression)
    {
        var target = Convert(expression.Target);

        // if the type does not support null then shortcut to true
        Expression converted =
            target.Type.IsValueType && Nullable.GetUnderlyingType(target.Type) is null
            ? Expression.Constant(true, typeof(bool))
            : Expression.NotEqual(target, Expression.Constant(null, target.Type));

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitEqual(EqualExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.Equal(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitNotEqual(NotEqualExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.NotEqual(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitAnd(AndExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.And(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitAndAlso(AndAlsoExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.AndAlso(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitOr(OrExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.Or(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitOrElse(OrElseExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.OrElse(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitLessThan(LessThanExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.LessThan(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitLessThanOrEqual(LessThanOrEqualExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.LessThanOrEqual(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitGreaterThan(GreaterThanExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.GreaterThan(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitAdd(AddExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.Add(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitGreaterThanOrEqual(GreaterThanOrEqualExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.GreaterThanOrEqual(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitStringContains(StringContainsExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var comparison = Expression.Constant(expression.Comparison, typeof(StringComparison));

        MethodInfo method;
        if (value.Type == typeof(string))
        {
            method = ((Func<string, StringComparison, bool>)"".Contains).Method;
        }
        else if (value.Type == typeof(char))
        {
            method = ((Func<char, StringComparison, bool>)"".Contains).Method;
        }
        else
        {
            throw new NotSupportedException($"Type '{value.Type.FullName}' is not supported as a value for {nameof(StringContainsExpression)}");
        }

        var converted = Expression.Call(target, method, value, comparison);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitStringCompare(StringCompareExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var comparison = Expression.Constant(expression.Comparison, typeof(StringComparison));
        var method = ((Func<string, string, StringComparison, int>)string.Compare).Method;
        var converted = Expression.Call(target, method, value, comparison);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitStringStartsWith(StringStartsWithExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var comparison = Expression.Constant(expression.Comparison, typeof(StringComparison));

        MethodInfo method;
        if (value.Type == typeof(string))
        {
            method = ((Func<string, StringComparison, bool>)"".StartsWith).Method;
        }
        else if (value.Type == typeof(char))
        {
            method = ((Func<char, bool>)"".StartsWith).Method;
        }
        else
        {
            throw new NotSupportedException($"Type '{value.Type.FullName}' is not supported as a value for {nameof(StringStartsWithExpression)}");
        }

        var converted = Expression.Call(target, method, value, comparison);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitStringEndsWith(StringEndsWithExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var comparison = Expression.Constant(expression.Comparison, typeof(StringComparison));

        MethodInfo method;
        if (value.Type == typeof(string))
        {
            method = ((Func<string, StringComparison, bool>)"".EndsWith).Method;
        }
        else if (value.Type == typeof(char))
        {
            method = ((Func<char, bool>)"".EndsWith).Method;
        }
        else
        {
            throw new NotSupportedException($"Type '{value.Type.FullName}' is not supported as a value for {nameof(StringEndsWithExpression)}");
        }

        var converted = Expression.Call(target, method, value, comparison);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpression expression)
    {
        var target = Convert(expression.Target);
        var method = ((Func<string, bool>)string.IsNullOrWhiteSpace).Method;
        var converted = Expression.Call(target, method);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitStringEqual(StringEqualExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var comparison = Expression.Constant(expression.Comparison, typeof(StringComparison));
        var method = ((Func<string, string, StringComparison, bool>)string.Equals).Method;
        var converted = Expression.Call(target, method, value, comparison);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitAssign(AssignExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var converted = Expression.Assign(target, value);

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitConstant<TValue>(ConstantExpression<TValue> expression)
    {
        var converted = Expression.Constant(expression.Value, typeof(TValue));

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitHashSet<TValue>(HashSetExpression<TValue> expression)
    {
        var converted = Expression.Constant(expression.Values, expression.Values.GetType());

        _stack.Push(converted);

        return expression;
    }

    protected internal override QueryExpression VisitContains(ContainsExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var result = Expression.Call(target, "Contains", null, value);

        _stack.Push(result);

        return expression;
    }
}