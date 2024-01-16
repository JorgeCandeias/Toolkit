using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents the decrementing of the expression by 1.
/// </summary>
public sealed record class DecrementWireExpression : WireExpression
{
    internal DecrementWireExpression(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        Expression = expression;
    }

    /// <summary>
    /// The expression to convert.
    /// </summary>
    public WireExpression Expression { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitDecrement(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents the decrementing of the expression by 1.
    /// </summary>
    public static DecrementWireExpression Decrement(WireExpression expression) => new(expression);
}