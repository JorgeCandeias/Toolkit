using System.Collections.Immutable;
using System.Reflection;

namespace Outcompute.Toolkit.Expressions.Visitors;

/// <summary>
/// Quality-of-life extensions for <see cref="LinqWireExpressionVisitor{T}"/>.
/// </summary>
internal static class LinqWireExpressionVisitor
{
    public static (Expression Result, ParameterExpression Item) Visit<T>(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        var visitor = new LinqWireExpressionVisitor<T>();

        visitor.Visit(expression);

        return (visitor.Result, visitor.Item);
    }

    public static Expression Visit<T>(WireExpression expression, ParameterExpression item)
    {
        Guard.IsNotNull(expression);
        Guard.IsNotNull(item);

        var visitor = new LinqWireExpressionVisitor<T>(item);

        visitor.Visit(expression);

        return visitor.Result;
    }
}

/// <summary>
/// Implements a <see cref="WireExpressionVisitor"/> that transforms <see cref="WireExpression"/> trees into LINQ <see cref="Expression"/> trees.
/// </summary>
internal sealed class LinqWireExpressionVisitor<T> : WireExpressionVisitor
{
    public LinqWireExpressionVisitor()
    {
        Item = Expression.Parameter(typeof(T), "item");
    }

    public LinqWireExpressionVisitor(ParameterExpression item)
    {
        Guard.IsNotNull(item);

        Item = item;
    }

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
    public ParameterExpression Item { get; }

    /// <summary>
    /// Converts the target <see cref="WireExpression"/> to a LINQ <see cref="Expression"/>.
    /// </summary>
    private Expression Convert(WireExpression expression)
    {
        // visit the expression
        // this will push the converted expression to the stack
        Visit(expression);

        // pop and return the converted expression from the stack
        return _stack.Pop();
    }

    protected internal override WireExpression VisitDefault(DefaultWireExpression expression)
    {
        _stack.Push(Expression.Empty());

        return expression;
    }

    protected internal override WireExpression VisitItem(ItemWireExpression expression)
    {
        _stack.Push(Item);

        return expression;
    }

    protected internal override WireExpression VisitAdd(AddWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.Add(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitAddAssign(AddAssignWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.AddAssign(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitAddAssignChecked(AddAssignCheckedWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.AddAssignChecked(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitAddChecked(AddCheckedWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.AddChecked(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitAnd(AndWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.And(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitAndAlso(AndAlsoWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.AndAlso(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitAndAssign(AndAssignWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.AndAssign(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitArray<TValue>(ArrayWireExpression<TValue> expression)
    {
        var converted = Expression.Constant(expression.Values);

        _stack.Push(converted);

        return expression;
    }








    protected internal override WireExpression VisitProperty(PropertyExpression expression)
    {
        var name = expression.Name;
        var target = expression.Target is ItemWireExpression ? Item : Convert(expression.Target);
        var converted = Expression.Property(target, name);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitField(FieldExpression expression)
    {
        var name = expression.Name;
        var target = expression.Target is ItemWireExpression ? Item : Convert(expression.Target);
        var converted = Expression.Field(target, name);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitPropertyOrField(PropertyOrFieldExpression expression)
    {
        var name = expression.Name;
        var target = expression.Target is ItemWireExpression ? Item : Convert(expression.Target);
        var converted = Expression.PropertyOrField(target, name);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitNot(NotExpression expression)
    {
        var target = Convert(expression.Target);
        var converted = Expression.Not(target);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitIsNull(IsNullExpression expression)
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

    protected internal override WireExpression VisitIsNotNull(IsNotNullExpression expression)
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

    protected internal override WireExpression VisitEqual(EqualWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.Equal(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitNotEqual(NotEqualWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.NotEqual(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitOr(OrExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.Or(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitOrElse(OrElseExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.OrElse(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitLessThan(LessThanWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.LessThan(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitLessThanOrEqual(LessThanOrEqualExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.LessThanOrEqual(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitGreaterThan(GreaterThanWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.GreaterThan(left, right);

        _stack.Push(converted);

        return expression;
    }



    protected internal override WireExpression VisitGreaterThanOrEqual(GreaterThanOrEqualWireExpression expression)
    {
        var left = Convert(expression.Left);
        var right = Convert(expression.Right);
        var converted = Expression.GreaterThanOrEqual(left, right);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitStringContains(StringContainsExpression expression)
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

    protected internal override WireExpression VisitStringCompare(StringCompareExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var comparison = Expression.Constant(expression.Comparison, typeof(StringComparison));
        var method = ((Func<string, string, StringComparison, int>)string.Compare).Method;
        var converted = Expression.Call(target, method, value, comparison);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitStringStartsWith(StringStartsWithExpression expression)
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

    protected internal override WireExpression VisitStringEndsWith(StringEndsWithExpression expression)
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

    protected internal override WireExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpression expression)
    {
        var target = Convert(expression.Target);
        var method = ((Func<string, bool>)string.IsNullOrWhiteSpace).Method;
        var converted = Expression.Call(target, method);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitStringEqual(StringEqualExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var comparison = Expression.Constant(expression.Comparison, typeof(StringComparison));
        var method = ((Func<string, string, StringComparison, bool>)string.Equals).Method;
        var converted = Expression.Call(target, method, value, comparison);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitAssign(AssignWireExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var converted = Expression.Assign(target, value);

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitConstant<TValue>(ConstantExpression<TValue> expression)
    {
        var converted = Expression.Constant(expression.Value, typeof(TValue));

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitHashSet<TValue>(HashSetExpression<TValue> expression)
    {
        var converted = Expression.Constant(expression.Values, expression.Values.GetType());

        _stack.Push(converted);

        return expression;
    }

    protected internal override WireExpression VisitContains(ContainsExpression expression)
    {
        var target = Convert(expression.Target);
        var value = Convert(expression.Value);
        var result = Expression.Call(target, "Contains", null, value);

        _stack.Push(result);

        return expression;
    }
}