using Outcompute.Toolkit.Expressions;
using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Protobuf.Expressions;

/// <summary>
/// Implements a <see cref="WireExpressionVisitor"/> that transforms a <see cref="WireExpression"/> tree into a <see cref="QueryExpressionSurrogate"/> tree.
/// </summary>
internal sealed class ProtobufQueryExpressionVisitor : WireExpressionVisitor
{
    private readonly Stack<QueryExpressionSurrogate> _stack = new();

    public QueryExpressionSurrogate Result => _stack.Peek();

    /// <summary>
    /// Converts the target <see cref="WireExpression"/> to a protobuf surrogate.
    /// </summary>
    private QueryExpressionSurrogate Convert(WireExpression expression)
    {
        // visit the expression
        // this will push the converted expression to the stack
        Visit(expression);

        // pop and return the converted expression from the stack
        return _stack.Pop();
    }

    protected override WireExpression VisitDefault<TValue>(DefaultWireExpression<TValue> expression)
    {
        var converted = new DefaultExpressionSurrogate<TValue>();

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitItem(ItemWireExpression expression)
    {
        var converted = new ItemExpressionSurrogate();

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitProperty(PropertyExpression expression)
    {
        var converted = new PropertyExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Name = expression.Name
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitField(FieldWireExpression expression)
    {
        var converted = new FieldExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Name = expression.Name
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitPropertyOrField(PropertyOrFieldExpression expression)
    {
        var converted = new PropertyOrFieldExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Name = expression.Name
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitNot(NotWireExpression expression)
    {
        var surrogate = new NotExpressionSurrogate
        {
            Target = Convert(expression.Expression)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitIsNull(IsNullExpression expression)
    {
        var surrogate = new IsNullExpressionSurrogate
        {
            Target = Convert(expression.Target)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitIsNotNull(IsNotNullExpression expression)
    {
        var surrogate = new IsNotNullExpressionSurrogate
        {
            Target = Convert(expression.Target)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitNotEqual(NotEqualWireExpression expression)
    {
        var converted = new NotEqualExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitOr(OrWireExpression expression)
    {
        var converted = new OrExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitOrElse(OrElseExpression expression)
    {
        var converted = new OrElseExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitLessThan(LessThanWireExpression expression)
    {
        var converted = new LessThanExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitLessThanOrEqual(LessThanOrEqualWireExpression expression)
    {
        var converted = new LessThanOrEqualExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitGreaterThan(GreaterThanWireExpression expression)
    {
        var converted = new GreaterThanExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitGreaterThanOrEqual(GreaterThanOrEqualWireExpression expression)
    {
        var converted = new GreaterThanOrEqualExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override WireExpression VisitStringContains(StringContainsExpression expression)
    {
        var surrogate = new StringContainsExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Value = Convert(expression.Value),
            Comparison = expression.Comparison
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitStringCompare(StringCompareExpression expression)
    {
        var surrogate = new StringCompareExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Value = Convert(expression.Value),
            Comparison = expression.Comparison
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitStringStartsWith(StringStartsWithExpression expression)
    {
        var surrogate = new StringStartsWithExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Value = Convert(expression.Value),
            Comparison = expression.Comparison
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitStringEndsWith(StringEndsWithExpression expression)
    {
        var surrogate = new StringEndsWithExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Value = Convert(expression.Value),
            Comparison = expression.Comparison
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpression expression)
    {
        var surrogate = new StringIsNullOrWhiteSpaceExpressionSurrogate
        {
            Target = Convert(expression.Target)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitStringEqual(StringEqualExpression expression)
    {
        var surrogate = new StringEqualExpressionSurrogate
        {
            Target = Convert(expression.Target)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitConstant<TValue>(ConstantWireExpression<TValue> expression)
    {
        var surrogate = new ConstantExpressionSurrogate<TValue>
        {
            Value = expression.Value
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitHashSet<TValue>(HashSetExpression<TValue> expression)
    {
        var surrogate = new HashSetExpressionSurrogate<TValue>
        {
            Values = expression.Values
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override WireExpression VisitContains(ContainsExpression expression)
    {
        var surrogate = new ContainsExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Value = Convert(expression.Value)
        };

        _stack.Push(surrogate);

        return expression;
    }
}