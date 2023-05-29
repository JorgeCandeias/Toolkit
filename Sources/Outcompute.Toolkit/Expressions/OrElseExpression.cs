using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a boolean 'OrElse' expression.
/// Written in C# as '||'.
/// 'OrElse' always evaluates the left-hand side and skips the right-hand side if the left-hand side evaluates to true.
/// </summary>
public sealed record class OrElseExpression : QueryExpression
{
    internal OrElseExpression(QueryExpression left, QueryExpression right)
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
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitOrElse(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="OrElseExpression"/> with the specified parameters.
    /// </summary>
    public static OrElseExpression OrElse(QueryExpression left, QueryExpression right) => new(left, right);

    /// <summary>
    /// Attempts to create a new <see cref="OrElseExpression"/> using all supplied arguments as operands.
    /// If <paramref name="expressions"/> is empty then this method returns null.
    /// If <paramref name="expressions"/> has a single expression then this method returns that expression.
    /// </summary>
    public static QueryExpression? OrElse(IEnumerable<QueryExpression> expressions)
    {
        Guard.IsNotNull(expressions);

        var enumerator = expressions.GetEnumerator();

        if (enumerator.MoveNext())
        {
            var expression = enumerator.Current;

            while (enumerator.MoveNext())
            {
                expression = OrElse(expression, enumerator.Current);
            }

            return expression;
        }

        return null;
    }
}