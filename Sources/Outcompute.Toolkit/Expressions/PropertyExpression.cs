using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a property accessor expression.
/// </summary>
public sealed record class PropertyExpression : WireExpression
{
    internal PropertyExpression(WireExpression target, string name)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(name);

        Target = target;
        Name = name;
    }

    /// <summary>
    /// The target for the property accessor.
    /// </summary>
    public WireExpression Target { get; }

    /// <summary>
    /// The name of the property to access.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitProperty(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="PropertyExpression"/> with the specified parameters.
    /// </summary>
    public static PropertyExpression Property(WireExpression target, string name) => new(target, name);

    /// <summary>
    /// Creates a new <see cref="PropertyExpression"/> where the target is the default iteration item.
    /// </summary>
    public static PropertyExpression ItemProperty(string name) => Property(Item(), name);
}