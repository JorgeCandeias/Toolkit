using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a .Contains() call.
/// Written in C# as <c>target.Contains(item)</c>.
/// </summary>
public sealed record class ContainsExpression : QueryExpression
{
    internal ContainsExpression(QueryExpression target, QueryExpression value)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(value);

        Target = target;
        Value = value;
    }

    /// <summary>
    /// The target of the call.
    /// </summary>
    public QueryExpression Target { get; }

    /// <summary>
    /// The value to look for.
    /// </summary>
    public QueryExpression Value { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitContains(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="ContainsExpression"/> with the specified parameters.
    /// </summary>
    public static ContainsExpression Contains(QueryExpression target, QueryExpression value) => new(target, value);
}