using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Protobuf.Expressions;

/// <summary>
/// Implements a visitor that transforms <see cref="QueryExpressionSurrogate"/> trees into <see cref="QueryExpression"/> trees.
/// This is the reverse visitor of <see cref="ProtobufQueryExpressionVisitor"/>.
/// </summary>
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Visitor Pattern")]
internal sealed class ProtobufQueryExpressionSurrogateVisitor
{
    /// <summary>
    /// Defers discovery of the correct Visit* overload to the specific expression.
    /// </summary>
    public QueryExpression Visit(QueryExpressionSurrogate expression)
    {
        return expression.Accept(this);
    }

    public DefaultExpression VisitDefault(DefaultExpressionSurrogate _)
    {
        return QueryExpression.Default();
    }

    public ItemExpression VisitItem(ItemExpressionSurrogate _)
    {
        return QueryExpression.Item();
    }

    public PropertyExpression VisitProperty(PropertyExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var name = surrogate.Name;

        return QueryExpression.Property(target, name);
    }

    public FieldExpression VisitField(FieldExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var name = surrogate.Name;

        return QueryExpression.Field(target, name);
    }

    public PropertyOrFieldExpression VisitPropertyOrField(PropertyOrFieldExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var name = surrogate.Name;

        return QueryExpression.PropertyOrField(target, name);
    }

    public NotExpression VisitNot(NotExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);

        return QueryExpression.Not(target);
    }

    public IsNullExpression VisitIsNull(IsNullExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);

        return QueryExpression.IsNull(target);
    }

    public IsNotNullExpression VisitIsNotNull(IsNotNullExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);

        return QueryExpression.IsNotNull(target);
    }

    public EqualExpression VisitEqual(EqualExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.Equal(left, right);
    }

    public NotEqualExpression VisitNotEqual(NotEqualExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.NotEqual(left, right);
    }

    public AndExpression VisitAnd(AndExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.And(left, right);
    }

    public AndAlsoExpression VisitAndAlso(AndAlsoExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.AndAlso(left, right);
    }

    public OrExpression VisitOr(OrExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.Or(left, right);
    }

    public OrElseExpression VisitOrElse(OrElseExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.OrElse(left, right);
    }

    public LessThanExpression VisitLessThan(LessThanExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.LessThan(left, right);
    }

    public LessThanOrEqualExpression VisitLessThanOrEqual(LessThanOrEqualExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.LessThanOrEqual(left, right);
    }

    public GreaterThanExpression VisitGreaterThan(GreaterThanExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.GreaterThan(left, right);
    }

    public GreaterThanOrEqualExpression VisitGreaterThanOrEqual(GreaterThanOrEqualExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.GreaterThanOrEqual(left, right);
    }

    public AddExpression VisitAdd(AddExpressionSurrogate surrogate)
    {
        var left = Visit(surrogate.Left);
        var right = Visit(surrogate.Right);

        return QueryExpression.Add(left, right);
    }

    public StringContainsExpression VisitStringContains(StringContainsExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return QueryExpression.StringContains(target, value, comparison);
    }

    public StringCompareExpression VisitStringCompare(StringCompareExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return QueryExpression.StringCompare(target, value, comparison);
    }

    public StringStartsWithExpression VisitStringStartsWith(StringStartsWithExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return QueryExpression.StringStartsWith(target, value, comparison);
    }

    public StringEndsWithExpression VisitStringEndsWith(StringEndsWithExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return QueryExpression.StringEndsWith(target, value, comparison);
    }

    public StringIsNullOrWhiteSpaceExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);

        return QueryExpression.StringIsNullOrWhiteSpace(target);
    }

    public StringEqualExpression VisitStringEqual(StringEqualExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);
        var comparison = surrogate.Comparison;

        return QueryExpression.StringEqual(target, value, comparison);
    }

    public AssignExpression VisitAssign(AssignExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);

        return QueryExpression.Assign(target, value);
    }

    public ConstantExpression<TValue> VisitConstant<TValue>(ConstantExpressionSurrogate<TValue> surrogate)
    {
        return QueryExpression.Constant(surrogate.Value);
    }

    public HashSetExpression<TValue> VisitHashSet<TValue>(HashSetExpressionSurrogate<TValue> surrogate)
    {
        return QueryExpression.HashSet(surrogate.Values);
    }

    public ContainsExpression VisitContains(ContainsExpressionSurrogate surrogate)
    {
        var target = Visit(surrogate.Target);
        var value = Visit(surrogate.Value);

        return QueryExpression.Contains(target, value);
    }
}