using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a String.Contains() call.
/// Written in C# as <c>"value".Contains("v", comparison)</c>.
/// </summary>
public sealed record class StringContainsExpression : QueryExpression
{
    internal StringContainsExpression(QueryExpression target, QueryExpression value, StringComparison comparison)
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
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitStringContains(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="StringContainsExpression"/> with the specified parameters.
    /// </summary>
    public static StringContainsExpression StringContains(QueryExpression target, QueryExpression value, StringComparison comparison = StringComparison.Ordinal) => new(target, value, comparison);
}