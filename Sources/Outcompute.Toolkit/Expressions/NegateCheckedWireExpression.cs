using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents an arithmetic negation operation that has overflow checking.
/// </summary>
public sealed record class NegateCheckedWireExpression : WireExpression
{
    internal NegateCheckedWireExpression(WireExpression expression)
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
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitNegateChecked(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic negation operation that has overflow checking.
    /// </summary>
    public static NegateCheckedWireExpression NegateChecked(WireExpression expression) => new(expression);
}