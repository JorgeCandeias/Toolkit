using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A placeholder for the enumerable iteration variable.
/// </summary>
public sealed record class DefaultWireExpression : WireExpression
{
    internal DefaultWireExpression()
    {
    }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitDefault(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="DefaultWireExpression"/> with the specified parameters.
    /// </summary>
    public static DefaultWireExpression Default() => new();
}