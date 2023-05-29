using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a boolean 'Less Than Or Equal' expression.
/// Written in C# as <c>left &lt;= right</c>.
/// </summary>
public sealed record class LessThanOrEqualExpression : QueryExpression
{
    internal LessThanOrEqualExpression(QueryExpression left, QueryExpression right)
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
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitLessThanOrEqual(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="LessThanOrEqualExpression"/> with the specified parameters.
    /// </summary>
    public static LessThanOrEqualExpression LessThanOrEqual(QueryExpression left, QueryExpression right) => new(left, right);
}