using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents a conversion operation that throws an exception if the target type is overflowed and for which the implementing method is specified.
/// </summary>
public sealed record class ConvertCheckedWireExpression<T> : WireExpression
{
    internal ConvertCheckedWireExpression(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        Expression = expression;
    }

    /// <summary>
    /// The expression to convert.
    /// </summary>
    public WireExpression Expression { get; }

    /// <summary>
    /// The type to convert to.
    /// </summary>
    public Type Type => typeof(T);

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitConvertChecked(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a conversion operation that throws an exception if the target type is overflowed and for which the implementing method is specified.
    /// </summary>
    public static ConvertCheckedWireExpression<T> ConvertChecked<T>(WireExpression expression) => new(expression);
}