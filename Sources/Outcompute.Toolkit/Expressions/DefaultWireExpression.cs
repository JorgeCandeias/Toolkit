using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents the default value of the specified type.
/// </summary>
public sealed record class DefaultWireExpression<T> : WireExpression
{
    internal DefaultWireExpression()
    {
    }

    /// <summary>
    /// The type to get the default of.
    /// </summary>
    public Type Type => typeof(T);

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitDefault(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents the default value of the specified type.
    /// </summary>
    public static DefaultWireExpression<T> Default<T>() => new();
}