namespace Outcompute.Toolkit.Expressions.Visitors;

public abstract class QueryExpressionVisitor
{
    /// <summary>
    /// Defers discovery of the correct Visit* overload to the specific expression.
    /// </summary>
    public QueryExpression Visit(QueryExpression expression)
    {
        Guard.IsNotNull(expression);

        return expression.Accept(this);
    }

    /// <summary>
    /// Default implementation of all visitor methods.
    /// Derived classes will inherit this behaviour for any expressions they do not support.
    /// </summary>
    protected QueryExpression ThrowNotSupported(QueryExpression expression)
    {
        Guard.IsNotNull(expression);

        return ThrowHelper.ThrowNotSupportedException<QueryExpression>($"Visitor '{GetType().FullName}' does not support expression of type '{expression.GetType().Name}'");
    }

    /// <summary>
    /// Visits the <see cref="DefaultExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitDefault(DefaultExpression expression);

    /// <summary>
    /// Visits the <see cref="ConstantExpression{T}"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitConstant<T>(ConstantExpression<T> expression);

    /// <summary>
    /// Visits the <see cref="ItemExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitItem(ItemExpression expression);

    /// <summary>
    /// Visits the <see cref="PropertyExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitProperty(PropertyExpression expression);

    /// <summary>
    /// Visits the <see cref="FieldExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitField(FieldExpression expression);

    /// <summary>
    /// Visits the <see cref="PropertyOrFieldExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitPropertyOrField(PropertyOrFieldExpression expression);

    /// <summary>
    /// Visits the <see cref="NotExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitNot(NotExpression expression);

    /// <summary>
    /// Visits the <see cref="IsNullExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitIsNull(IsNullExpression expression);

    /// <summary>
    /// Visits the <see cref="IsNotNullExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitIsNotNull(IsNotNullExpression expression);

    /// <summary>
    /// Visits the <see cref="EqualExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitEqual(EqualExpression expression);

    /// <summary>
    /// Visits the <see cref="NotEqualExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitNotEqual(NotEqualExpression expression);

    /// <summary>
    /// Visits the <see cref="AndExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitAnd(AndExpression expression);

    /// <summary>
    /// Visits the <see cref="AndAlsoExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitAndAlso(AndAlsoExpression expression);

    /// <summary>
    /// Visits the <see cref="OrExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitOr(OrExpression expression);

    /// <summary>
    /// Visits the <see cref="OrElseExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitOrElse(OrElseExpression expression);

    /// <summary>
    /// Visits the <see cref="LessThanExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitLessThan(LessThanExpression expression);

    /// <summary>
    /// Visits the <see cref="LessThanOrEqualExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitLessThanOrEqual(LessThanOrEqualExpression expression);

    /// <summary>
    /// Visits the <see cref="GreaterThanExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitGreaterThan(GreaterThanExpression expression);

    /// <summary>
    /// Visits the <see cref="GreaterThanOrEqualExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitGreaterThanOrEqual(GreaterThanOrEqualExpression expression);

    /// <summary>
    /// Visits the <see cref="AddExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitAdd(AddExpression expression);

    /// <summary>
    /// Visits the <see cref="StringContainsExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitStringContains(StringContainsExpression expression);

    /// <summary>
    /// Visits the <see cref="StringCompareExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitStringCompare(StringCompareExpression expression);

    /// <summary>
    /// Visits the <see cref="StringStartsWithExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitStringStartsWith(StringStartsWithExpression expression);

    /// <summary>
    /// Visits the <see cref="StringEndsWithExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitStringEndsWith(StringEndsWithExpression expression);

    /// <summary>
    /// Visits the <see cref="StringIsNullOrWhiteSpaceExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpression expression);

    /// <summary>
    /// Visits the <see cref="StringEqualExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitStringEqual(StringEqualExpression expression);

    /// <summary>
    /// Visits the <see cref="AssignExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitAssign(AssignExpression expression);

    /// <summary>
    /// Visits the <see cref="HashSetExpression{TValue}"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitHashSet<TValue>(HashSetExpression<TValue> expression);

    /// <summary>
    /// Visits the <see cref="ContainsExpression"/>.
    /// </summary>
    protected internal abstract QueryExpression VisitContains(ContainsExpression expression);
}