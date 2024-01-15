using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents a bitwise AND operation.
/// </summary>
public sealed record class AndWireExpression : WireExpression
{
    internal AndWireExpression(WireExpression left, WireExpression right)
    {
        Guard.IsNotNull(left);
        Guard.IsNotNull(right);

        Left = left;
        Right = right;
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
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitAnd(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a bitwise AND operation.
    /// </summary>
    public static AndWireExpression And(WireExpression left, WireExpression right) => new(left, right);

    /// <summary>
    /// Attempts to creates a <see cref="WireExpression"/> that represents a bitwise AND operation using the specified operands.
    /// If <paramref name="expressions"/> is empty then this method returns null.
    /// If <paramref name="expressions"/> has a single expression then this method returns that expression.
    /// </summary>
    public static WireExpression? And(IEnumerable<WireExpression> expressions)
    {
        Guard.IsNotNull(expressions);

        var enumerator = expressions.GetEnumerator();

        if (enumerator.MoveNext())
        {
            var expression = enumerator.Current;

            while (enumerator.MoveNext())
            {
                expression = And(expression, enumerator.Current);
            }

            return expression;
        }

        return null;
    }
}