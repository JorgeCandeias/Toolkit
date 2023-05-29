using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Represents a placeholder for the enumerable iteration variable.
/// </summary>
public sealed record class ItemExpression : QueryExpression
{
    internal ItemExpression()
    {
    }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitItem(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="ItemExpression"/> with the specified parameters.
    /// </summary>
    public static ItemExpression Item() => new();
}