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
    /// Visits the <see cref="ArrayWireExpression{T}"/>.
    /// </summary>
    protected internal virtual WireExpression VisitArray<T>(ArrayWireExpression<T> expression) => ThrowNotSupported(expression);

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
    /// Visits the <see cref="EmptyWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitEmpty(EmptyWireExpression expression) => ThrowNotSupported(expression);

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

    /// <summary>
    /// Visits the <see cref="LeftShiftWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitLeftShift(LeftShiftWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="LeftShiftAssignWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitLeftShiftAssign(LeftShiftAssignWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="LessThanWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitLessThan(LessThanWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="LessThanOrEqualWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitLessThanOrEqual(LessThanOrEqualWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ModuloWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitModulo(ModuloWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="ModuloAssignWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitModuloAssign(ModuloAssignWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="MultiplyWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitMultiply(MultiplyWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="MultiplyAssignWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitMultiplyAssign(MultiplyAssignWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="MultiplyAssignCheckedWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitMultiplyAssignChecked(MultiplyAssignCheckedWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="MultiplyCheckedWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitMultiplyChecked(MultiplyCheckedWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="NegateWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitNegate(NegateWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="NegateCheckedWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitNegateChecked(NegateCheckedWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="NotWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitNot(NotWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="NotEqualWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitNotEqual(NotEqualWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="OnesComplementWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitOnesComplement(OnesComplementWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="OrWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitOr(OrWireExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="BinaryWireExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitBinary(BinaryWireExpression expression) => ThrowNotSupported(expression);
















    #region Equality


    #endregion Equality

    #region Comparison


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
    /// Visits the <see cref="IsNullExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitIsNull(IsNullExpression expression) => ThrowNotSupported(expression);

    /// <summary>
    /// Visits the <see cref="IsNotNullExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitIsNotNull(IsNotNullExpression expression) => ThrowNotSupported(expression);


    /// <summary>
    /// Visits the <see cref="OrElseExpression"/>.
    /// </summary>
    protected internal virtual WireExpression VisitOrElse(OrElseExpression expression) => ThrowNotSupported(expression);


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