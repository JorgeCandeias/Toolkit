using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// An empty <see cref="WireExpression"/>.
/// </summary>
public sealed record class EmptyWireExpression : WireExpression
{
    internal EmptyWireExpression()
    {
    }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitEmpty(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates an empty <see cref="WireExpression"/>.
    /// </summary>
    public static EmptyWireExpression Empty() => new();
}