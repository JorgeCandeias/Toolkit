namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Represents a single criterion in an OrderBy() operation.
/// </summary>
/// <remarks>
/// This class is immutable.
/// </remarks>
public sealed class OrderByCriterion
{
    private OrderByCriterion(WireExpression expression, OrderByDirection direction)
    {
        Expression = expression;
        Direction = direction;
    }

    /// <summary>
    /// The expression to order by.
    /// </summary>
    public WireExpression Expression { get; }

    /// <summary>
    /// The direction to order by.
    /// </summary>
    public OrderByDirection Direction { get; }

    /// <summary>
    /// Creates a new <see cref="OrderByCriterion"/> with the specified parameters.
    /// </summary>
    public static OrderByCriterion Create(WireExpression expression, OrderByDirection direction) => new(expression, direction);
}