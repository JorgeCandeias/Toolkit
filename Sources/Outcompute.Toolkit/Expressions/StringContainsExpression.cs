using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a String.Contains() call.
/// Written in C# as <c>"value".Contains("v", comparison)</c>.
/// </summary>
public sealed record class StringContainsExpression : WireExpression
{
    internal StringContainsExpression(WireExpression target, WireExpression value, StringComparison comparison)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(value);

        Target = target;
        Value = value;
        Comparison = comparison;
    }

    /// <summary>
    /// The target of the Contains() call.
    /// </summary>
    public WireExpression Target { get; }

    /// <summary>
    /// The value to look for.
    /// </summary>
    public WireExpression Value { get; }

    /// <summary>
    /// The comparison rule to use.
    /// </summary>
    public StringComparison Comparison { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitStringContains(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="StringContainsExpression"/> with the specified parameters.
    /// </summary>
    public static StringContainsExpression StringContains(WireExpression target, WireExpression value, StringComparison comparison = StringComparison.Ordinal) => new(target, value, comparison);
}