using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a String.IsNullOrWhiteSpace() call.
/// Written in C# as <c>string.IsNullOrWhiteSpace(target)</c>.
/// </summary>
public sealed record class StringIsNullOrWhiteSpaceExpression : QueryExpression
{
    internal StringIsNullOrWhiteSpaceExpression(QueryExpression target)
    {
        Guard.IsNotNull(target);

        Target = target;
    }

    /// <summary>
    /// The target of the IsNullOrWhiteSpace() call.
    /// </summary>
    public QueryExpression Target { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitStringIsNullOrWhiteSpace(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="StringIsNullOrWhiteSpaceExpression"/> with the specified parameters.
    /// </summary>
    public static StringIsNullOrWhiteSpaceExpression StringIsNullOrWhiteSpace(QueryExpression target) => new(target);
}