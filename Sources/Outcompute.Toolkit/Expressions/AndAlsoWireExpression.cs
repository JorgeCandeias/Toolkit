using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a boolean 'AndAlso' expression.
/// Written in C# as '&&'.
/// 'AndAlso' always evaluates the left-hand side and skips the right-hand side if the left-hand side evaluates to false.
/// </summary>
public sealed record class AndAlsoWireExpression : WireExpression
{
    internal AndAlsoWireExpression(WireExpression left, WireExpression right)
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
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitAndAlso(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a conditional AND operation that evaluates the second operand only if the first operand evaluates to true.
    /// </summary>
    public static AndAlsoWireExpression AndAlso(WireExpression left, WireExpression right) => new(left, right);

    /// <summary>
    /// Attempts to creates a <see cref="WireExpression"/> that represents a conditional AND operation that evaluates the second operand only if the first operand evaluates to true, using the specified operands.
    /// If <paramref name="expressions"/> is empty then this method returns null.
    /// If <paramref name="expressions"/> has a single expression then this method returns that expression.
    /// </summary>
    public static WireExpression? AndAlso(IEnumerable<WireExpression> expressions)
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