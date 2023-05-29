using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a String.Equal() call.
/// Written in C# as <c>string.Equal("left", "right", comparison)</c>.
/// </summary>
public sealed record class StringEqualExpression : QueryExpression
{
    internal StringEqualExpression(QueryExpression target, QueryExpression value, StringComparison comparison)
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
    public QueryExpression Target { get; }

    /// <summary>
    /// The value to look for.
    /// </summary>
    public QueryExpression Value { get; }

    /// <summary>
    /// The comparison rule to use.
    /// </summary>
    public StringComparison Comparison { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitStringEqual(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="StringEqualExpression"/> with the specified parameters.
    /// </summary>
    public static StringEqualExpression StringEqual(QueryExpression target, QueryExpression value, StringComparison comparison = StringComparison.Ordinal) => new(target, value, comparison);
}