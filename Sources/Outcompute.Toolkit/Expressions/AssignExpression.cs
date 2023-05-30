using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines an assignment expression.
/// Written in C# as <c>target = value</c>.
/// </summary>
public sealed record class AssignExpression : WireExpression
{
    internal AssignExpression(WireExpression target, WireExpression value)
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
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitAssign(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="AssignExpression"/> with the specified parameters.
    /// </summary>
    public static AssignExpression Assign(WireExpression target, WireExpression value) => new(target, value);
}