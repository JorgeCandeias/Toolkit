﻿using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines an equality expression.
/// Written in C# as <c>==</c>.
/// </summary>
public sealed record class EqualExpression : WireExpression
{
    internal EqualExpression(WireExpression left, WireExpression right)
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
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitEqual(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="EqualExpression"/> with the specified parameters.
    /// </summary>
    public static EqualExpression Equal(WireExpression left, WireExpression right) => new(left, right);
}