using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a constant expression.
/// </summary>
public sealed record class ConstantExpression<T> : WireExpression
{
    internal ConstantExpression(T value)
    {
        Value = value;
    }

    /// <summary>
    /// The constant value.
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitConstant(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="ConstantExpression<T>"/> with the specified parameters.
    /// </summary>
    public static ConstantExpression<T> Constant<T>(T value) => new(value);
}