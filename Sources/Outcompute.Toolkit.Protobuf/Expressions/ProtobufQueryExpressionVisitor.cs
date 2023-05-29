using Outcompute.Toolkit.Expressions;
using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Protobuf.Expressions;

/// <summary>
/// Implements a <see cref="QueryExpressionVisitor"/> that transforms a <see cref="QueryExpression"/> tree into a <see cref="QueryExpressionSurrogate"/> tree.
/// </summary>
internal sealed class ProtobufQueryExpressionVisitor : QueryExpressionVisitor
{
    private readonly Stack<QueryExpressionSurrogate> _stack = new();

    public QueryExpressionSurrogate Result => _stack.Peek();

    /// <summary>
    /// Converts the target <see cref="QueryExpression"/> to a protobuf surrogate.
    /// </summary>
    private QueryExpressionSurrogate Convert(QueryExpression expression)
    {
        // visit the expression
        // this will push the converted expression to the stack
        Visit(expression);

        // pop and return the converted expression from the stack
        return _stack.Pop();
    }

    protected override QueryExpression VisitDefault(DefaultExpression expression)
    {
        var converted = new DefaultExpressionSurrogate();

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitItem(ItemExpression expression)
    {
        var converted = new ItemExpressionSurrogate();

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitProperty(PropertyExpression expression)
    {
        var converted = new PropertyExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Name = expression.Name
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitField(FieldExpression expression)
    {
        var converted = new FieldExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Name = expression.Name
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitPropertyOrField(PropertyOrFieldExpression expression)
    {
        var converted = new PropertyOrFieldExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Name = expression.Name
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitNot(NotExpression expression)
    {
        var surrogate = new NotExpressionSurrogate
        {
            Target = Convert(expression.Target)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override QueryExpression VisitIsNull(IsNullExpression expression)
    {
        var surrogate = new IsNullExpressionSurrogate
        {
            Target = Convert(expression.Target)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override QueryExpression VisitIsNotNull(IsNotNullExpression expression)
    {
        var surrogate = new IsNotNullExpressionSurrogate
        {
            Target = Convert(expression.Target)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override QueryExpression VisitEqual(EqualExpression expression)
    {
        var converted = new EqualExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitNotEqual(NotEqualExpression expression)
    {
        var converted = new NotEqualExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitAnd(AndExpression expression)
    {
        var converted = new AndExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitAndAlso(AndAlsoExpression expression)
    {
        var converted = new AndAlsoExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitOr(OrExpression expression)
    {
        var converted = new OrExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitOrElse(OrElseExpression expression)
    {
        var converted = new OrElseExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitLessThan(LessThanExpression expression)
    {
        var converted = new LessThanExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitLessThanOrEqual(LessThanOrEqualExpression expression)
    {
        var converted = new LessThanOrEqualExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitGreaterThan(GreaterThanExpression expression)
    {
        var converted = new GreaterThanExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitGreaterThanOrEqual(GreaterThanOrEqualExpression expression)
    {
        var converted = new GreaterThanOrEqualExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitAdd(AddExpression expression)
    {
        var converted = new AddExpressionSurrogate
        {
            Left = Convert(expression.Left),
            Right = Convert(expression.Right)
        };

        _stack.Push(converted);

        return expression;
    }

    protected override QueryExpression VisitStringContains(StringContainsExpression expression)
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

    protected override QueryExpression VisitStringCompare(StringCompareExpression expression)
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

    protected override QueryExpression VisitStringStartsWith(StringStartsWithExpression expression)
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

    protected override QueryExpression VisitStringEndsWith(StringEndsWithExpression expression)
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

    protected override QueryExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpression expression)
    {
        var surrogate = new StringIsNullOrWhiteSpaceExpressionSurrogate
        {
            Target = Convert(expression.Target)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override QueryExpression VisitStringEqual(StringEqualExpression expression)
    {
        var surrogate = new StringEqualExpressionSurrogate
        {
            Target = Convert(expression.Target)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override QueryExpression VisitAssign(AssignExpression expression)
    {
        var surrogate = new AssignExpressionSurrogate
        {
            Target = Convert(expression.Target),
            Value = Convert(expression.Value)
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override QueryExpression VisitConstant<TValue>(ConstantExpression<TValue> expression)
    {
        var surrogate = new ConstantExpressionSurrogate<TValue>
        {
            Value = expression.Value
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override QueryExpression VisitHashSet<TValue>(HashSetExpression<TValue> expression)
    {
        var surrogate = new HashSetExpressionSurrogate<TValue>
        {
            Values = expression.Values
        };

        _stack.Push(surrogate);

        return expression;
    }

    protected override QueryExpression VisitContains(ContainsExpression expression)
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