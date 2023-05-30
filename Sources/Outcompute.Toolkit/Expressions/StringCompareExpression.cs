using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a String.Compare() call.
/// Written in C# as <c>string.Compare("left", "right", comparison)</c>.
/// </summary>
public sealed record class StringCompareExpression : WireExpression
{
    internal StringCompareExpression(WireExpression target, WireExpression value, StringComparison comparison)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(value);

        Target = target;
        Value = value;
        Comparison = comparison;
    }

    /// <summary>
    /// The target of the Compare() call.
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
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitStringCompare(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="StringCompareExpression"/> with the specified parameters.
    /// </summary>
    public static StringCompareExpression StringCompare(WireExpression target, WireExpression value, StringComparison comparison = StringComparison.Ordinal) => new(target, value, comparison);
}