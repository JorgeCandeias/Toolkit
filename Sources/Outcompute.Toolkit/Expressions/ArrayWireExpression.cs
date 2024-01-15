using Outcompute.Toolkit.Expressions.Visitors;
using System.Collections.Immutable;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents a typed array.
/// </summary>
public sealed record class ArrayWireExpression<T> : WireExpression
{
    internal ArrayWireExpression(ImmutableArray<T> value)
    {
        Values = value;
    }

    /// <summary>
    /// The constant value.
    /// </summary>
    public ImmutableArray<T> Values { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitArray(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a typed array.
    /// </summary>
    public static ArrayWireExpression<T> Array<T>(ImmutableArray<T> value) => new(value);
}