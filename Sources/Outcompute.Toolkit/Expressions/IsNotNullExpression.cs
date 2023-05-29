using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a test for null.
/// Written in C# as <c>value != null</c>.
/// </summary>
public sealed record class IsNotNullExpression : QueryExpression
{
    internal IsNotNullExpression(QueryExpression target)
    {
        Guard.IsNotNull(target);

        Target = target;
    }

    /// <summary>
    /// The target of the not null test.
    /// </summary>
    public QueryExpression Target { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitIsNotNull(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="IsNotNullExpression"/> with the specified parameters.
    /// </summary>
    public static IsNotNullExpression IsNotNull(QueryExpression target) => new(target);
}