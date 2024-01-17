using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Creates a <see cref="WireExpression"/> that represents a bitwise complement operation. The implementing method can be specified.
/// </summary>
public sealed record class NotWireExpression : WireExpression
{
    internal NotWireExpression(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        Expression = expression;
    }

    /// <summary>
    /// The target of the negation operation.
    /// </summary>
    public WireExpression Expression { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitNot(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a bitwise complement operation. The implementing method can be specified.
    /// </summary>
    public static NotWireExpression Not(WireExpression target) => new(target);
}