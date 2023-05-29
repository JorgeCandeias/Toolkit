using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a property accessor expression.
/// </summary>
public sealed record class PropertyExpression : QueryExpression
{
    internal PropertyExpression(QueryExpression target, string name)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(name);

        Target = target;
        Name = name;
    }

    /// <summary>
    /// The target for the property accessor.
    /// </summary>
    public QueryExpression Target { get; }

    /// <summary>
    /// The name of the property to access.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitProperty(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="PropertyExpression"/> with the specified parameters.
    /// </summary>
    public static PropertyExpression Property(QueryExpression target, string name) => new(target, name);

    /// <summary>
    /// Creates a new <see cref="PropertyExpression"/> where the target is the default iteration item.
    /// </summary>
    public static PropertyExpression ItemProperty(string name) => Property(Item(), name);
}