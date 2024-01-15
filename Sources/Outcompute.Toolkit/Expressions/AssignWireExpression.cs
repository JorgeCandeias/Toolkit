using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a <see cref="WireExpression"/> that represents an assignment operation.
/// </summary>
public sealed record class AssignWireExpression : WireExpression
{
    internal AssignWireExpression(WireExpression target, WireExpression value)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(value);

        Target = target;
        Value = target;
    }

    /// <summary>
    /// The left-hand side expression.
    /// </summary>
    public WireExpression Target { get; }

    /// <summary>
    /// The right-hand side expression.
    /// </summary>
    public WireExpression Value { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitAssign(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an assignment operation.
    /// </summary>
    public static AssignWireExpression Assign(WireExpression target, WireExpression value) => new(target, value);
}