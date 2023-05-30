using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Represents a placeholder for the enumerable iteration variable.
/// </summary>
public sealed record class ItemExpression : WireExpression
{
    internal ItemExpression()
    {
    }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitItem(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="ItemExpression"/> with the specified parameters.
    /// </summary>
    public static ItemExpression Item() => new();
}