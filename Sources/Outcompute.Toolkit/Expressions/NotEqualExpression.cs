using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines an non-equality expression.
/// Written in C# as <c>!=</c>.
/// </summary>
public sealed record class NotEqualExpression : QueryExpression
{
    internal NotEqualExpression(QueryExpression left, QueryExpression right)
    {
        Guard.IsNotNull(left);
        Guard.IsNotNull(right);

        Left = left;
        Right = left;
    }

    /// <summary>
    /// The left-hand side expression.
    /// </summary>
    public QueryExpression Left { get; }

    /// <summary>
    /// The right-hand side expression.
    /// </summary>
    public QueryExpression Right { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitNotEqual(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="NotEqualExpression"/> with the specified parameters.
    /// </summary>
    public static NotEqualExpression NotEqual(QueryExpression left, QueryExpression right) => new(left, right);
}