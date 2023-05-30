using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a boolean 'Greater Than' expression.
/// Written in C# as <c>left &gt; right</c>.
/// </summary>
public sealed record class GreaterThanExpression : WireExpression
{
    internal GreaterThanExpression(WireExpression left, WireExpression right)
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
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitGreaterThan(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="GreaterThanExpression"/> with the specified parameters.
    /// </summary>
    public static GreaterThanExpression GreaterThan(WireExpression left, WireExpression right) => new(left, right);
}