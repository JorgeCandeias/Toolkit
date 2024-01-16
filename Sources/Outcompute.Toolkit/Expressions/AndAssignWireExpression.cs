﻿using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents an addition assignment operation that does not have overflow checking.
/// </summary>
public sealed record class AndAssignWireExpression : WireExpression
{
    internal AndAssignWireExpression(WireExpression left, WireExpression right)
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
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitAndAssign(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a bitwise AND assignment operation.
    /// </summary>
    public static AndAssignWireExpression AndAssign(WireExpression left, WireExpression right) => new(left, right);
}