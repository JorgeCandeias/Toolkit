using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents a type conversion operation.
/// </summary>
public sealed record class ConvertWireExpression<T> : WireExpression
{
    internal ConvertWireExpression(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        Expression = expression;
    }

    /// <summary>
    /// The expression to convert.
    /// </summary>
    public WireExpression Expression { get; }

    /// <summary>
    /// The type to convert to.
    /// </summary>
    public Type Type => typeof(T);

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitConvert(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a type conversion operation.
    /// </summary>
    public static ConvertWireExpression<T> Convert<T>(WireExpression expression) => new(expression);
}