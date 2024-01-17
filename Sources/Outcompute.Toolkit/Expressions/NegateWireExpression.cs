using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents an arithmetic negation operation.
/// </summary>
public sealed record class NegateWireExpression : WireExpression
{
    internal NegateWireExpression(WireExpression expression)
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
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitNegate(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic negation operation.
    /// </summary>
    public static NegateWireExpression Negate(WireExpression expression) => new(expression);
}