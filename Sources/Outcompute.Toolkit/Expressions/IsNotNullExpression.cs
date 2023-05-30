using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a test for null.
/// Written in C# as <c>value != null</c>.
/// </summary>
public sealed record class IsNotNullExpression : WireExpression
{
    internal IsNotNullExpression(WireExpression target)
    {
        Guard.IsNotNull(target);

        Target = target;
    }

    /// <summary>
    /// The target of the not null test.
    /// </summary>
    public WireExpression Target { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitIsNotNull(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="IsNotNullExpression"/> with the specified parameters.
    /// </summary>
    public static IsNotNullExpression IsNotNull(WireExpression target) => new(target);
}