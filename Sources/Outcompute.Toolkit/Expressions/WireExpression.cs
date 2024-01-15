using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines the base class for all serializable expressions.
/// </summary>
public abstract partial record class WireExpression
{
    private protected WireExpression()
    {
    }

    /// <summary>
    /// Derived expressions implement this method to redirect the call to the correct visitor method.
    /// </summary>
    protected internal abstract WireExpression Accept(WireExpressionVisitor visitor);

    /// <summary>
    /// Renders the expression tree as a readable string.
    /// </summary>
    /// <remarks>
    /// This is meant to aid troubleshooting.
    /// It is not meant to support text form parsing.
    /// </remarks>
    public sealed override string ToString()
    {
        using var visitor = new StringWireExpressionVisitor();

        visitor.Visit(this);

        return visitor.ToString();
    }
}