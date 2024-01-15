using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a boolean 'Less Than Or Equal' expression.
/// Written in C# as <c>left &lt;= right</c>.
/// </summary>
public sealed record class LessThanOrEqualExpression : WireExpression
{
    internal LessThanOrEqualExpression(WireExpression left, WireExpression right)
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
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitLessThanOrEqual(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="LessThanOrEqualExpression"/> with the specified parameters.
    /// </summary>
    public static LessThanOrEqualExpression LessThanOrEqual(WireExpression left, WireExpression right) => new(left, right);
}