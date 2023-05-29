using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a boolean 'Greater Than Or Equal' expression.
/// Written in C# as <c>left &gt;= right</c>.
/// </summary>
public sealed record class GreaterThanOrEqualExpression : QueryExpression
{
    internal GreaterThanOrEqualExpression(QueryExpression left, QueryExpression right)
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
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitGreaterThanOrEqual(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="GreaterThanOrEqualExpression"/> with the specified parameters.
    /// </summary>
    public static GreaterThanOrEqualExpression GreaterThanOrEqual(QueryExpression left, QueryExpression right) => new(left, right);
}