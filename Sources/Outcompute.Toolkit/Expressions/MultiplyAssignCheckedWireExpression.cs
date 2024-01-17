using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents a multiplication assignment operation that has overflow checking.
/// </summary>
public sealed record class MultiplyAssignCheckedWireExpression : WireExpression
{
    internal MultiplyAssignCheckedWireExpression(WireExpression left, WireExpression right)
    {
        Guard.IsNotNull(left);
        Guard.IsNotNull(right);

        Left = left;
        Right = right;
    }

    /// <summary>
    /// The left-hand side expression.
    /// </summary>
    public WireExpression Left { get; }

    /// <summary>
    /// The right-hand side expression.
    /// </summary>
    public WireExpression Right { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitMultiplyAssignChecked(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a multiplication assignment operation that has overflow checking.
    /// </summary>
    public static MultiplyAssignCheckedWireExpression MultiplyAssignChecked(WireExpression left, WireExpression right) => new(left, right);
}