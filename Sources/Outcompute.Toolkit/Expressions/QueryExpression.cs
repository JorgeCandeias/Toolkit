using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines the base class for all toolkit expressions.
/// </summary>
public abstract partial record class QueryExpression
{
    private protected QueryExpression()
    {
    }

    /// <summary>
    /// Derived expressions implement this method to redirect the call to the correct visitor method.
    /// </summary>
    protected internal abstract QueryExpression Accept(QueryExpressionVisitor visitor);

    /// <summary>
    /// Renders the expression tree as a readable string.
    /// </summary>
    public override string ToString()
    {
        using var visitor = new StringQueryExpressionVisitor();

        visitor.Visit(this);

        return visitor.ToString();
    }
}