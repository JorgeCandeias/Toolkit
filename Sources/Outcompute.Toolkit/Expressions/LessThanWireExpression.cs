using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents a "less than" numeric comparison.
/// </summary>
public sealed record class LessThanWireExpression : WireExpression
{
    internal LessThanWireExpression(WireExpression left, WireExpression right, bool liftToNull)
    {
        Guard.IsNotNull(left);
        Guard.IsNotNull(right);

        Left = left;
        Right = right;
        IsLiftedToNull = liftToNull;
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
    /// Whether the operation is lifted to a nullable type.
    /// </summary>
    public bool IsLiftedToNull { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitLessThan(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a "less than" numeric comparison.
    /// </summary>
    public static LessThanWireExpression LessThan(WireExpression left, WireExpression right, bool liftToNull = false) => new(left, right, liftToNull);
}