using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Protobuf.Expressions;

/// <summary>
/// Implements a visitor that transforms <see cref="QueryExpressionSurrogate"/> trees into <see cref="WireExpression"/> trees.
/// This is the reverse visitor of <see cref="ProtobufQueryExpressionVisitor"/>.
/// </summary>
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Visitor Pattern")]
internal sealed class ProtobufQueryExpressionSurrogateVisitor
{
    /// <summary>
    /// Defers discovery of the correct Visit* overload to the specific expression.
    /// </summary>
    public WireExpression Visit(QueryExpressionSurrogate expression)
    {
        return expression.Accept(this);
    }

    public DefaultExpression VisitDefault(DefaultExpressionSurrogate _)
    {
        return WireExpression.Default();
    }

    public ItemExpression VisitItem(ItemExpressionSurrogate _)
    {
        return WireExpression.Item();
    }

    public PropertyExpression VisitProperty(PropertyExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var name = surrogate.Name;

        return WireExpression.Property(target, name);
    }

    public FieldExpression VisitField(FieldExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var name = surrogate.Name;

        return WireExpression.Field(target, name);
    }

    public PropertyOrFieldExpression VisitPropertyOrField(PropertyOrFieldExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var name = surrogate.Name;

        return WireExpression.PropertyOrField(target, name);
    }

    public NotExpression VisitNot(NotExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);

        return WireExpression.Not(target);
    }

    public IsNullExpression VisitIsNull(IsNullExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);

        return WireExpression.IsNull(target);
    }

    public IsNotNullExpression VisitIsNotNull(IsNotNullExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);

        return WireExpression.IsNotNull(target);
    }

    public EqualExpression VisitEqual(EqualExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.Equal(left, right);
    }

    public NotEqualExpression VisitNotEqual(NotEqualExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.NotEqual(left, right);
    }

    public AndExpression VisitAnd(AndExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.And(left, right);
    }

    public AndAlsoExpression VisitAndAlso(AndAlsoExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.AndAlso(left, right);
    }

    public OrExpression VisitOr(OrExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.Or(left, right);
    }

    public OrElseExpression VisitOrElse(OrElseExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.OrElse(left, right);
    }

    public LessThanExpression VisitLessThan(LessThanExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.LessThan(left, right);
    }

    public LessThanOrEqualExpression VisitLessThanOrEqual(LessThanOrEqualExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.LessThanOrEqual(left, right);
    }

    public GreaterThanExpression VisitGreaterThan(GreaterThanExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.GreaterThan(left, right);
    }

    public GreaterThanOrEqualExpression VisitGreaterThanOrEqual(GreaterThanOrEqualExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.GreaterThanOrEqual(left, right);
    }

    public AddExpression VisitAdd(AddExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return WireExpression.Add(left, right);
    }

    public StringContainsExpression VisitStringContains(StringContainsExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return WireExpression.StringContains(target, value, comparison);
    }

    public StringCompareExpression VisitStringCompare(StringCompareExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return WireExpression.StringCompare(target, value, comparison);
    }

    public StringStartsWithExpression VisitStringStartsWith(StringStartsWithExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return WireExpression.StringStartsWith(target, value, comparison);
    }

    public StringEndsWithExpression VisitStringEndsWith(StringEndsWithExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return WireExpression.StringEndsWith(target, value, comparison);
    }

    public StringIsNullOrWhiteSpaceExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);

        return WireExpression.StringIsNullOrWhiteSpace(target);
    }

    public StringEqualExpression VisitStringEqual(StringEqualExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return WireExpression.StringEqual(target, value, comparison);
    }

    public AssignExpression VisitAssign(AssignExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);

        return WireExpression.Assign(target, value);
    }

    public ConstantExpression<TValue> VisitConstant<TValue>(ConstantExpressionSurrogate<TValue> surrogate)
    {
        return WireExpression.Constant(surrogate.Value);
    }

    public HashSetExpression<TValue> VisitHashSet<TValue>(HashSetExpressionSurrogate<TValue> surrogate)
    {
        return WireExpression.HashSet(surrogate.Values);
    }

    public ContainsExpression VisitContains(ContainsExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);

        return WireExpression.Contains(target, value);
    }
}