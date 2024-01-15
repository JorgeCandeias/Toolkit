using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a .Contains() call.
/// Written in C# as <c>target.Contains(item)</c>.
/// </summary>
public sealed record class ContainsExpression : WireExpression
{
    internal ContainsExpression(WireExpression target, WireExpression value)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(value);

        Target = target;
        Value = value;
    }

    /// <summary>
    /// The target of the call.
    /// </summary>
    public WireExpression Target { get; }

    /// <summary>
    /// The value to look for.
    /// </summary>
    public WireExpression Value { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitContains(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="ContainsExpression"/> with the specified parameters.
    /// </summary>
    public static ContainsExpression Contains(WireExpression target, WireExpression value) => new(target, value);
}