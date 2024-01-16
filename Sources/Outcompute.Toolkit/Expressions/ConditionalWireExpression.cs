using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// A <see cref="WireExpression"/> that represents a conditional statement.
/// </summary>
public sealed record class ConditionalWireExpression : WireExpression
{
    internal ConditionalWireExpression(WireExpression test, WireExpression ifTrue, WireExpression ifFalse)
    {
        Guard.IsNotNull(test);
        Guard.IsNotNull(ifTrue);
        Guard.IsNotNull(ifFalse);

        Test = test;
        IfTrue = ifTrue;
        IfFalse = ifFalse;
    }

    /// <summary>
    /// The expression to test.
    /// </summary>
    public WireExpression Test { get; }

    /// <summary>
    /// The expression returned if test is true.
    /// </summary>
    public WireExpression IfTrue { get; }

    /// <summary>
    /// The expression returned if test is false.
    /// </summary>
    public WireExpression IfFalse { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitCondition(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a <see cref="WireExpression"/> that represents a conditional statement.
    /// </summary>
    public static ConditionalWireExpression Condition(WireExpression test, WireExpression ifTrue, WireExpression ifFalse) => new(test, ifTrue, ifFalse);
}