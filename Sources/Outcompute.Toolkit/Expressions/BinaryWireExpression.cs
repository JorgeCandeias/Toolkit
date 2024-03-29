﻿using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that has a binary operator.
/// </summary>
public sealed record class BinaryWireExpression : WireExpression
{
    internal BinaryWireExpression(BinaryWireOperation operation, WireExpression left, WireExpression right, bool liftToNull = false)
    {
        Guard.IsNotNull(left);
        Guard.IsNotNull(right);

        Operation = operation;
        Left = left;
        Right = right;
        IsLiftedToNull = liftToNull;
    }

    /// <summary>
    /// Gets the binary operation represented by this expression.
    /// </summary>
    public BinaryWireOperation Operation { get; }

    /// <summary>
    /// Gets the left operand of the binary operation.
    /// </summary>
    public WireExpression Left { get; }

    /// <summary>
    /// Gets the right operand of the binary operation.
    /// </summary>
    public WireExpression Right { get; }

    /// <summary>
    /// Gets a value that indicates whether the expression tree node represents a lifted call to an operator whose return type is lifted to a nullable type.
    /// </summary>
    public bool IsLiftedToNull { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitBinary(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic addition operation that does not have overflow checking.
    /// </summary>
    public static BinaryWireExpression Add(WireExpression left, WireExpression right) => new(BinaryWireOperation.Add, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an addition assignment operation that does not have overflow checking.
    /// </summary>
    public static BinaryWireExpression AddAssign(WireExpression left, WireExpression right) => new(BinaryWireOperation.AddAssign, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an addition assignment operation that has overflow checking.
    /// </summary>
    public static BinaryWireExpression AddAssignChecked(WireExpression left, WireExpression right) => new(BinaryWireOperation.AddAssignChecked, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic addition operation that has overflow checking.
    /// </summary>
    public static BinaryWireExpression AddChecked(WireExpression left, WireExpression right) => new(BinaryWireOperation.AddChecked, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a conditional AND operation that evaluates the second operand only if the first operand evaluates to true.
    /// </summary>
    public static BinaryWireExpression AndAlso(WireExpression left, WireExpression right) => new(BinaryWireOperation.AndAlso, left, right);

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

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a bitwise AND assignment operation.
    /// </summary>
    public static BinaryWireExpression AndAssign(WireExpression left, WireExpression right) => new(BinaryWireOperation.AndAssign, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a bitwise AND operation.
    /// </summary>
    public static BinaryWireExpression And(WireExpression left, WireExpression right) => new(BinaryWireOperation.And, left, right);

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

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an assignment operation.
    /// </summary>
    public static BinaryWireExpression Assign(WireExpression target, WireExpression value) => new(BinaryWireOperation.Assign, target, value);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a coalescing operation.
    /// </summary>
    public static BinaryWireExpression Coalesce(WireExpression left, WireExpression right) => new(BinaryWireOperation.Coalesce, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic division operation.
    /// </summary>
    public static BinaryWireExpression Divide(WireExpression left, WireExpression right) => new(BinaryWireOperation.Divide, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a division assignment operation that does not have overflow checking.
    /// </summary>
    public static BinaryWireExpression DivideAssign(WireExpression left, WireExpression right) => new(BinaryWireOperation.DivideAssign, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an equality comparison.
    /// </summary>
    public static BinaryWireExpression Equal(WireExpression left, WireExpression right, bool liftToNull = false) => new(BinaryWireOperation.Equal, left, right, liftToNull);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a bitwise XOR operation, using op_ExclusiveOr for user-defined types.
    /// </summary>
    public static BinaryWireExpression ExclusiveOr(WireExpression left, WireExpression right) => new(BinaryWireOperation.ExclusiveOr, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a bitwise XOR assignment operation, using op_ExclusiveOr for user-defined types.
    /// </summary>
    public static BinaryWireExpression ExclusiveOrAssign(WireExpression left, WireExpression right) => new(BinaryWireOperation.ExclusiveOrAssign, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a "greater than" numeric comparison.
    /// </summary>
    public static BinaryWireExpression GreaterThan(WireExpression left, WireExpression right, bool liftToNull = false) => new(BinaryWireOperation.GreaterThan, left, right, liftToNull);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a "greater than or equal" numeric comparison.
    /// </summary>
    public static BinaryWireExpression GreaterThanOrEqual(WireExpression left, WireExpression right, bool liftToNull = false) => new(BinaryWireOperation.GreaterThanOrEqual, left, right, liftToNull);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a "less than" numeric comparison.
    /// </summary>
    public static BinaryWireExpression LessThan(WireExpression left, WireExpression right, bool liftToNull = false) => new(BinaryWireOperation.LessThan, left, right, liftToNull);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a "less than or equal" numeric comparison.
    /// </summary>
    public static BinaryWireExpression LessThanOrEqual(WireExpression left, WireExpression right, bool liftToNull = false) => new(BinaryWireOperation.LessThanOrEqual, left, right, liftToNull);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic remainder operation.
    /// </summary>
    public static BinaryWireExpression Modulo(WireExpression left, WireExpression right) => new(BinaryWireOperation.Modulo, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a remainder assignment operation.
    /// </summary>
    public static BinaryWireExpression ModuloAssign(WireExpression left, WireExpression right) => new(BinaryWireOperation.ModuloAssign, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic division operation.
    /// </summary>
    public static BinaryWireExpression Multiply(WireExpression left, WireExpression right) => new(BinaryWireOperation.Multiply, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic multiplication operation that has overflow checking.
    /// </summary>
    public static BinaryWireExpression MultiplyChecked(WireExpression left, WireExpression right) => new(BinaryWireOperation.MultiplyChecked, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an arithmetic division operation.
    /// </summary>
    public static BinaryWireExpression MultiplyAssign(WireExpression left, WireExpression right) => new(BinaryWireOperation.MultiplyAssign, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a multiplication assignment operation that has overflow checking.
    /// </summary>
    public static BinaryWireExpression MultiplyAssignChecked(WireExpression left, WireExpression right) => new(BinaryWireOperation.MultiplyAssignChecked, left, right);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents an inequality comparison.
    /// </summary>
    public static BinaryWireExpression NotEqual(WireExpression left, WireExpression right, bool liftToNull = false) => new(BinaryWireOperation.NotEqual, left, right, liftToNull);

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a bitwise OR operation.
    /// </summary>
    public static BinaryWireExpression Or(WireExpression left, WireExpression right) => new(BinaryWireOperation.Or, left, right);

    /// <summary>
    /// Attempts to create a new <see cref="WireExpression"/> that represents a bitwise OR operation using the specified operands.
    /// If <paramref name="expressions"/> is empty then this method returns null.
    /// If <paramref name="expressions"/> has a single expression then this method returns that expression.
    /// </summary>
    public static WireExpression? Or(IEnumerable<WireExpression> expressions)
    {
        Guard.IsNotNull(expressions);

        var enumerator = expressions.GetEnumerator();

        if (enumerator.MoveNext())
        {
            var expression = enumerator.Current;

            while (enumerator.MoveNext())
            {
                expression = Or(expression, enumerator.Current);
            }

            return expression;
        }

        return null;
    }

    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a conditional OR operation that evaluates the second operand only if the first operand evaluates to false.
    /// </summary>
    public static BinaryWireExpression OrElse(WireExpression left, WireExpression right) => new(BinaryWireOperation.OrElse, left, right);

    /// <summary>
    /// Attempts to create a <see cref="WireExpression"/> that represents a conditional OR operation that evaluates the second operand only if the first operand evaluates to false,
    /// using the specified expressions as operands.
    /// If <paramref name="expressions"/> is empty then this method returns null.
    /// If <paramref name="expressions"/> has a single expression then this method returns that expression.
    /// </summary>
    public static WireExpression? OrElse(IEnumerable<WireExpression> expressions)
    {
        Guard.IsNotNull(expressions);

        var enumerator = expressions.GetEnumerator();

        if (enumerator.MoveNext())
        {
            var expression = enumerator.Current;

            while (enumerator.MoveNext())
            {
                expression = OrElse(expression, enumerator.Current);
            }

            return expression;
        }

        return null;
    }
}