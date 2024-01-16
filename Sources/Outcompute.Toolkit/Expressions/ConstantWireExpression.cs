using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents a constant value.
/// </summary>
public sealed record class ConstantWireExpression<T> : WireExpression
{
    internal ConstantWireExpression(T value)
    {
        Value = value;
    }

    /// <summary>
    /// The constant value.
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitConstant(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a constant value.
    /// </summary>
    public static ConstantWireExpression<T> Constant<T>(T value) => new(value);
}