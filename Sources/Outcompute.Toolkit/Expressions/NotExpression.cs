using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines the logical negation of a target expression.
/// Written in C# as <c>!(value)</c>.
/// </summary>
public sealed record class NotExpression : QueryExpression
{
    internal NotExpression(QueryExpression target)
    {
        Guard.IsNotNull(target);

        Target = target;
    }

    /// <summary>
    /// The target of the negation operation.
    /// </summary>
    public QueryExpression Target { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitNot(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="NotExpression"/> with the specified parameters.
    /// </summary>
    public static NotExpression Not(QueryExpression target) => new(target);
}