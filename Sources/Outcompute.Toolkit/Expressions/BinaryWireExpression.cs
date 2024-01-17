using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that has a binary operator.
/// </summary>
public sealed record class BinaryWireExpression : WireExpression
{
    internal BinaryWireExpression(BinaryWireOperation operation, WireExpression left, WireExpression right, bool liftToNull = false)
    {
        Guard.IsNotNull(left);
        Guard.IsNotNull(right);

        Operation = operation;
        Left = left;
        Right = right;
        IsLiftedToNull = liftToNull;
    }

    /// <summary>
    /// Gets the binary operation represented by this expression.
    /// </summary>
    public BinaryWireOperation Operation { get; }

    /// <summary>
    /// Gets the left operand of the binary operation.
    /// </summary>
    public WireExpression Left { get; }

    /// <summary>
    /// Gets the right operand of the binary operation.
    /// </summary>
    public WireExpression Right { get; }

    /// <summary>
    /// Gets a value that indicates whether the expression tree node represents a lifted call to an operator whose return type is lifted to a nullable type.
    /// </summary>
    public bool IsLiftedToNull { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitBinary(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic addition operation that does not have overflow checking.
    /// </summary>
    public static BinaryWireExpression Add(WireExpression left, WireExpression right) => new(BinaryWireOperation.Add, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an addition assignment operation that does not have overflow checking.
    /// </summary>
    public static BinaryWireExpression AddAssign(WireExpression left, WireExpression right) => new(BinaryWireOperation.AddAssign, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an addition assignment operation that has overflow checking.
    /// </summary>
    public static BinaryWireExpression AddAssignChecked(WireExpression left, WireExpression right) => new(BinaryWireOperation.AddAssignChecked, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic addition operation that has overflow checking.
    /// </summary>
    public static BinaryWireExpression AddChecked(WireExpression left, WireExpression right) => new(BinaryWireOperation.AddChecked, left, right);

}