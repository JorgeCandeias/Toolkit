using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a String.StartsWith() call.
/// Written in C# as <c>"value".StartsWith("v", comparison)</c>.
/// </summary>
public sealed record class StringStartsWithExpression : WireExpression
{
    internal StringStartsWithExpression(WireExpression target, WireExpression value, StringComparison comparison)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(value);

        Target = target;
        Value = value;
        Comparison = comparison;
    }

    /// <summary>
    /// The target of the StartsWith() call.
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
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitStringStartsWith(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="StringStartsWithExpression"/> with the specified parameters.
    /// </summary>
    public static StringStartsWithExpression StringStartsWith(WireExpression target, WireExpression value, StringComparison comparison = StringComparison.Ordinal) => new(target, value, comparison);
}