using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a String.EndsWith() call.
/// Written in C# as <c>"value".EndsWith("v", comparison)</c>.
/// </summary>
public sealed record class StringEndsWithExpression : QueryExpression
{
    internal StringEndsWithExpression(QueryExpression target, QueryExpression value, StringComparison comparison)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(value);

        Target = target;
        Value = value;
        Comparison = comparison;
    }

    /// <summary>
    /// The target of the EndsWith() call.
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
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitStringEndsWith(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="StringEndsWithExpression"/> with the specified parameters.
    /// </summary>
    public static StringEndsWithExpression StringEndsWith(QueryExpression target, QueryExpression value, StringComparison comparison = StringComparison.Ordinal) => new(target, value, comparison);
}