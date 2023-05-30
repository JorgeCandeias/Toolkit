using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines the logical negation of a target expression.
/// Written in C# as <c>!(value)</c>.
/// </summary>
public sealed record class NotExpression : WireExpression
{
    internal NotExpression(WireExpression target)
    {
        Guard.IsNotNull(target);

        Target = target;
    }

    /// <summary>
    /// The target of the negation operation.
    /// </summary>
    public WireExpression Target { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitNot(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="NotExpression"/> with the specified parameters.
    /// </summary>
    public static NotExpression Not(WireExpression target) => new(target);
}