using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a boolean 'Or' expression.
/// Written in C# as '|'.
/// 'Or' always evaluates the left-hand and right-hand operators.
/// </summary>
public sealed record class OrExpression : QueryExpression
{
    internal OrExpression(QueryExpression left, QueryExpression right)
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
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitOr(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="OrExpression"/> with the specified parameters.
    /// </summary>
    public static OrExpression Or(QueryExpression left, QueryExpression right) => new(left, right);

    /// <summary>
    /// Attempts to create a new <see cref="OrExpression"/> using all supplied arguments as operands.
    /// If <paramref name="expressions"/> is empty then this method returns null.
    /// If <paramref name="expressions"/> has a single expression then this method returns that expression.
    /// </summary>
    public static QueryExpression? Or(IEnumerable<QueryExpression> expressions)
    {
        Guard.IsNotNull(expressions);

        var enumerator = expressions.GetEnumerator();

        if (enumerator.MoveNext())
        {
            var expression = enumerator.Current;

            while (enumerator.MoveNext())
            {
                expression = Or(expression, enumerator.Current);
            }

            return expression;
        }

        return null;
    }
}