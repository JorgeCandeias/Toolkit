using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines an addition expression.
/// Written in C# as <c>left + right</c>.
/// </summary>
public sealed record class AddExpression : QueryExpression
{
    internal AddExpression(QueryExpression left, QueryExpression right)
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
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitAdd(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="AddExpression"/> with the specified parameters.
    /// </summary>
    public static AddExpression Add(QueryExpression left, QueryExpression right) => new(left, right);
}