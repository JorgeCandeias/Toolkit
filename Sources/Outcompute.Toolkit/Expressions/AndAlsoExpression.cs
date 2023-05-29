using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a boolean 'AndAlso' expression.
/// Written in C# as '&&'.
/// 'AndAlso' always evaluates the left-hand side and skips the right-hand side if the left-hand side evaluates to false.
/// </summary>
public sealed record class AndAlsoExpression : QueryExpression
{
    internal AndAlsoExpression(QueryExpression left, QueryExpression right)
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
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitAndAlso(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="AndAlsoExpression"/> with the specified parameters.
    /// </summary>
    public static AndAlsoExpression AndAlso(QueryExpression left, QueryExpression right) => new(left, right);

    /// <summary>
    /// Attempts to create a new <see cref="AndAlsoExpression"/> using all supplied arguments as operands.
    /// If <paramref name="expressions"/> is empty then this method returns null.
    /// If <paramref name="expressions"/> has a single expression then this method returns that expression.
    /// </summary>
    public static QueryExpression? AndAlso(IEnumerable<QueryExpression> expressions)
    {
        Guard.IsNotNull(expressions);

        var enumerator = expressions.GetEnumerator();

        if (enumerator.MoveNext())
        {
            var expression = enumerator.Current;

            while (enumerator.MoveNext())
            {
                expression = AndAlso(expression, enumerator.Current);
            }

            return expression;
        }

        return null;
    }
}