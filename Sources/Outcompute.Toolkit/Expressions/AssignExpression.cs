using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines an assignment expression.
/// Written in C# as <c>target = value</c>.
/// </summary>
public sealed record class AssignExpression : QueryExpression
{
    internal AssignExpression(QueryExpression target, QueryExpression value)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(value);

        Target = target;
        Value = target;
    }

    /// <summary>
    /// The left-hand side expression.
    /// </summary>
    public QueryExpression Target { get; }

    /// <summary>
    /// The right-hand side expression.
    /// </summary>
    public QueryExpression Value { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitAssign(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="AssignExpression"/> with the specified parameters.
    /// </summary>
    public static AssignExpression Assign(QueryExpression target, QueryExpression value) => new(target, value);
}