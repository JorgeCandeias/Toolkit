using Outcompute.Toolkit.Expressions.Visitors;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Defines a test for null.
/// Written in C# as <c>value == null</c>.
/// </summary>
public sealed record class IsNullExpression : QueryExpression
{
    internal IsNullExpression(QueryExpression target)
    {
        Guard.IsNotNull(target);

        Target = target;
    }

    /// <summary>
    /// The target of the null test.
    /// </summary>
    public QueryExpression Target { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override QueryExpression Accept(QueryExpressionVisitor visitor) => visitor.VisitIsNull(this);
}

public partial record class QueryExpression
{
    /// <summary>
    /// Creates a new <see cref="IsNullExpression"/> with the specified parameters.
    /// </summary>
    public static IsNullExpression IsNull(QueryExpression target) => new(target);
}