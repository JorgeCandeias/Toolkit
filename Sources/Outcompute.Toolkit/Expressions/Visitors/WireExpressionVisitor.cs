namespace Outcompute.Toolkit.Expressions.Visitors;

/// <summary>
/// Defines the expected structure of visitors of a <see cref="WireExpression"/> tree.
/// Custom visitors should inherit from this class and override all supported Visit*() methods.
/// Any method not implemented will throw an appropriate <see cref="NotSupportedException"/> by default.
/// Derived classes can also throw such an appropriate exception by calling <see cref="ThrowNotSupported(WireExpression)"/>.
/// </summary>
public abstract class WireExpressionVisitor
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

    #region Placeholder Expressions

    /// <summary>
    /// Visits the <see cref="ItemWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitItem(ItemWireExpression expression) => ThrowNotSupported(expression);

    #endregion Placeholder Expressions

    /// <summary>
    /// Visits the <see cref="AddWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitAdd(AddWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="AddAssignWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitAddAssign(AddAssignWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="AddAssignCheckedWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitAddAssignChecked(AddAssignCheckedWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="AddCheckedWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitAddChecked(AddCheckedWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="AndWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitAnd(AndWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="AndAlsoWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitAndAlso(AndAlsoWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="AndAssignWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitAndAssign(AndAssignWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ArrayWireExpression{T}"/>.
    /// </summary>
    protected internal virtual WireExpression VisitArray<T>(ArrayWireExpression<T> expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="AssignWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitAssign(AssignWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="CoalesceWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitCoalesce(CoalesceWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ConditionalWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitCondition(ConditionalWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ConstantWireExpression{TValue}"/>.
    /// </summary>
    protected internal virtual WireExpression VisitConstant<TValue>(ConstantWireExpression<TValue> expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ConvertWireExpression{TValue}"/>.
    /// </summary>
    protected internal virtual WireExpression VisitConvert<TValue>(ConvertWireExpression<TValue> expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ConvertCheckedWireExpression{TValue}"/>.
    /// </summary>
    protected internal virtual WireExpression VisitConvertChecked<TValue>(ConvertCheckedWireExpression<TValue> expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="DecrementWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitDecrement(DecrementWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="DefaultWireExpression{T}"/>.
    /// </summary>
    protected internal virtual WireExpression VisitDefault<TValue>(DefaultWireExpression<TValue> expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="DivideWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitDivide(DivideWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="DivideAssignWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitDivideAssign(DivideAssignWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="EmptyWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitEmpty(EmptyWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="EqualWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitEqual(EqualWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ExclusiveOrWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitExclusiveOr(ExclusiveOrWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ExclusiveOrAssignWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitExclusiveOrAssign(ExclusiveOrAssignWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="FieldWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitField(FieldWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="GreaterThanWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitGreaterThan(GreaterThanWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="GreaterThanOrEqualWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitGreaterThanOrEqual(GreaterThanOrEqualWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="IncrementWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitIncrement(IncrementWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="IsFalseWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitIsFalse(IsFalseWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="IsTrueWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitIsTrue(IsTrueWireExpression expression) => ThrowNotSupported(expression);













    #region Equality

    /// <summary>
    /// Visits the <see cref="NotEqualWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitNotEqual(NotEqualWireExpression expression) => ThrowNotSupported(expression);

    #endregion Equality

    #region Comparison

    /// <summary>
    /// Visits the <see cref="LessThanWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitLessThan(LessThanWireExpression expression) => ThrowNotSupported(expression);

    #endregion Comparison

    /// <summary>
    /// Visits the <see cref="PropertyExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitProperty(PropertyExpression expression) => ThrowNotSupported(expression);


    /// <summary>
    /// Visits the <see cref="PropertyOrFieldExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitPropertyOrField(PropertyOrFieldExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="NotExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitNot(NotExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="IsNullExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitIsNull(IsNullExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="IsNotNullExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitIsNotNull(IsNotNullExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="OrExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitOr(OrExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="OrElseExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitOrElse(OrElseExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="LessThanOrEqualExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitLessThanOrEqual(LessThanOrEqualExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="StringContainsExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitStringContains(StringContainsExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="StringCompareExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitStringCompare(StringCompareExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="StringStartsWithExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitStringStartsWith(StringStartsWithExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="StringEndsWithExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitStringEndsWith(StringEndsWithExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="StringIsNullOrWhiteSpaceExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="StringEqualExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitStringEqual(StringEqualExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="HashSetExpression{TValue}"/>.
    /// </summary>
    protected internal virtual WireExpression VisitHashSet<TValue>(HashSetExpression<TValue> expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ContainsExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitContains(ContainsExpression expression) => ThrowNotSupported(expression);
}