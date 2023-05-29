using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a field accessor expression.
/// </summary>
public sealed record class FieldExpression : QueryExpression
{
    internal FieldExpression(QueryExpression target, string name)
    {
        Guard.IsNotNull(target);
        Guard.IsNotNull(name);

        Target = target;
        Name = name;
    }

    /// <summary>
    /// The target for the field accessor.
    /// </summary>
    public QueryExpression Target { get; }

    /// <summary>
    /// The name of the field to access.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitField(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="FieldExpression"/> with the specified parameters.
    /// </summary>
    public static FieldExpression Field(QueryExpression target, string name) => new(target, name);

    /// <summary>
    /// Creates a new <see cref="FieldExpression"/> where the target is the default iteration item.
    /// </summary>
    public static FieldExpression ItemField(string name) => Field(Item(), name);
}