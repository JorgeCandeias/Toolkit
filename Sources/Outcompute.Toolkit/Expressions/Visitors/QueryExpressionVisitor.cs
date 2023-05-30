namespace Outcompute.Toolkit.Expressions.Visitors;

public abstract class QueryExpressionVisitor
{
    /// <summary>
    /// Defers discovery of the correct Visit* overload to the specific expression.
    /// </summary>
    public WireExpression Visit(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        return expression.Accept(this);
    }

    /// <summary>
    /// Default implementation of all visitor methods.
    /// Derived classes will inherit this behaviour for any expressions they do not support.
    /// </summary>
    protected WireExpression ThrowNotSupported(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        return ThrowHelper.ThrowNotSupportedException<WireExpression>($"Visitor '{GetType().FullName}' does not support expression of type '{expression.GetType().Name}'");
    }

    /// <summary>
    /// Visits the <see cref="DefaultExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitDefault(DefaultExpression expression);

    /// <summary>
    /// Visits the <see cref="ConstantExpression{T}"/>.
    /// </summary>
    protected internal abstract WireExpression VisitConstant<T>(ConstantExpression<T> expression);

    /// <summary>
    /// Visits the <see cref="ItemExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitItem(ItemExpression expression);

    /// <summary>
    /// Visits the <see cref="PropertyExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitProperty(PropertyExpression expression);

    /// <summary>
    /// Visits the <see cref="FieldExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitField(FieldExpression expression);

    /// <summary>
    /// Visits the <see cref="PropertyOrFieldExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitPropertyOrField(PropertyOrFieldExpression expression);

    /// <summary>
    /// Visits the <see cref="NotExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitNot(NotExpression expression);

    /// <summary>
    /// Visits the <see cref="IsNullExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitIsNull(IsNullExpression expression);

    /// <summary>
    /// Visits the <see cref="IsNotNullExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitIsNotNull(IsNotNullExpression expression);

    /// <summary>
    /// Visits the <see cref="EqualExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitEqual(EqualExpression expression);

    /// <summary>
    /// Visits the <see cref="NotEqualExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitNotEqual(NotEqualExpression expression);

    /// <summary>
    /// Visits the <see cref="AndExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitAnd(AndExpression expression);

    /// <summary>
    /// Visits the <see cref="AndAlsoExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitAndAlso(AndAlsoExpression expression);

    /// <summary>
    /// Visits the <see cref="OrExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitOr(OrExpression expression);

    /// <summary>
    /// Visits the <see cref="OrElseExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitOrElse(OrElseExpression expression);

    /// <summary>
    /// Visits the <see cref="LessThanExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitLessThan(LessThanExpression expression);

    /// <summary>
    /// Visits the <see cref="LessThanOrEqualExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitLessThanOrEqual(LessThanOrEqualExpression expression);

    /// <summary>
    /// Visits the <see cref="GreaterThanExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitGreaterThan(GreaterThanExpression expression);

    /// <summary>
    /// Visits the <see cref="GreaterThanOrEqualExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitGreaterThanOrEqual(GreaterThanOrEqualExpression expression);

    /// <summary>
    /// Visits the <see cref="AddExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitAdd(AddExpression expression);

    /// <summary>
    /// Visits the <see cref="StringContainsExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitStringContains(StringContainsExpression expression);

    /// <summary>
    /// Visits the <see cref="StringCompareExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitStringCompare(StringCompareExpression expression);

    /// <summary>
    /// Visits the <see cref="StringStartsWithExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitStringStartsWith(StringStartsWithExpression expression);

    /// <summary>
    /// Visits the <see cref="StringEndsWithExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitStringEndsWith(StringEndsWithExpression expression);

    /// <summary>
    /// Visits the <see cref="StringIsNullOrWhiteSpaceExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpression expression);

    /// <summary>
    /// Visits the <see cref="StringEqualExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitStringEqual(StringEqualExpression expression);

    /// <summary>
    /// Visits the <see cref="AssignExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitAssign(AssignExpression expression);

    /// <summary>
    /// Visits the <see cref="HashSetExpression{TValue}"/>.
    /// </summary>
    protected internal abstract WireExpression VisitHashSet<TValue>(HashSetExpression<TValue> expression);

    /// <summary>
    /// Visits the <see cref="ContainsExpression"/>.
    /// </summary>
    protected internal abstract WireExpression VisitContains(ContainsExpression expression);
}