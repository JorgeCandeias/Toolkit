using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience expression that represents a String.IsNullOrWhiteSpace() call.
/// Written in C# as <c>string.IsNullOrWhiteSpace(target)</c>.
/// </summary>
public sealed record class StringIsNullOrWhiteSpaceExpression : WireExpression
{
    internal StringIsNullOrWhiteSpaceExpression(WireExpression target)
    {
        Guard.IsNotNull(target);

        Target = target;
    }

    /// <summary>
    /// The target of the IsNullOrWhiteSpace() call.
    /// </summary>
    public WireExpression Target { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitStringIsNullOrWhiteSpace(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="StringIsNullOrWhiteSpaceExpression"/> with the specified parameters.
    /// </summary>
    public static StringIsNullOrWhiteSpaceExpression StringIsNullOrWhiteSpace(WireExpression target) => new(target);
}