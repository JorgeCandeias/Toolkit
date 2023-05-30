using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a boolean 'Or' expression.
/// Written in C# as '|'.
/// 'Or' always evaluates the left-hand and right-hand operators.
/// </summary>
public sealed record class OrExpression : WireExpression
{
    internal OrExpression(WireExpression left, WireExpression right)
    {
        Guard.IsNotNull(left);
        Guard.IsNotNull(right);

        Left = left;
        Right = left;
    }

    /// <summary>
    /// The left-hand side expression.
    /// </summary>
    public WireExpression Left { get; }

    /// <summary>
    /// The right-hand side expression.
    /// </summary>
    public WireExpression Right { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitOr(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="OrExpression"/> with the specified parameters.
    /// </summary>
    public static OrExpression Or(WireExpression left, WireExpression right) => new(left, right);

    /// <summary>
    /// Attempts to create a new <see cref="OrExpression"/> using all supplied arguments as operands.
    /// If <paramref name="expressions"/> is empty then this method returns null.
    /// If <paramref name="expressions"/> has a single expression then this method returns that expression.
    /// </summary>
    public static WireExpression? Or(IEnumerable<WireExpression> expressions)
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