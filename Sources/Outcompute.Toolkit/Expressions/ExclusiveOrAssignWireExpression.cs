using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Creates a <see cref="WireExpression"/> that represents a bitwise XOR assignment operation, using op_ExclusiveOr for user-defined types.
/// </summary>
public sealed record class ExclusiveOrAssignWireExpression : WireExpression
{
    internal ExclusiveOrAssignWireExpression(WireExpression left, WireExpression right)
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
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitExclusiveOrAssign(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a bitwise XOR assignment operation, using op_ExclusiveOr for user-defined types.
    /// </summary>
    public static ExclusiveOrAssignWireExpression ExclusiveOrAssign(WireExpression left, WireExpression right) => new(left, right);
}