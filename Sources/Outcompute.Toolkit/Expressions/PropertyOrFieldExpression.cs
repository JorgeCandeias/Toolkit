using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a property or field accessor expression.
/// </summary>
public sealed record class PropertyOrFieldExpression : QueryExpression
{
    internal PropertyOrFieldExpression(QueryExpression target, string name)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(name);

        Target = target;
        Name = name;
    }

    /// <summary>
    /// The target for property or field accessor.
    /// </summary>
    public QueryExpression Target { get; }

    /// <summary>
    /// The name of the property or field to access.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitPropertyOrField(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="PropertyOrFieldExpression"/> with the specified parameters.
    /// </summary>
    public static PropertyOrFieldExpression PropertyOrField(QueryExpression target, string name) => new(target, name);

    /// <summary>
    /// Creates a new <see cref="PropertyOrFieldExpression"/> where the target is the default iteration item.
    /// </summary>
    public static PropertyOrFieldExpression ItemPropertyOrField(string name) => PropertyOrField(Item(), name);
}