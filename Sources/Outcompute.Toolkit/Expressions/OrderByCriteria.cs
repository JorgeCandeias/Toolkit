using System.Collections.Immutable;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience holder, builder and validator of a full order-by criteria.
/// </summary>
/// <remarks>
/// This class is immutable.
/// </remarks>
public sealed class OrderByCriteria
{
    private OrderByCriteria(ImmutableArray<OrderByCriterion> criterions)
    {
        foreach (var item in criterions)
        {
            if (item is null)
            {
                throw new ArgumentException($"Null criterions are not allowed");
            }
        }

        Criterions = criterions;
    }

    /// <summary>
    /// The criterions that form this criteria.
    /// </summary>
    public ImmutableArray<OrderByCriterion> Criterions { get; }

    /// <summary>
    /// Gets a new <see cref="OrderByCriteria"/> with the current criterions plus the specified <paramref name="criterion"/> added at the end.
    /// </summary>
    public OrderByCriteria ThenBy(OrderByCriterion criterion)
    {
        Guard.IsNotNull(criterion);

        return new OrderByCriteria(Criterions.Add(criterion));
    }

    /// <summary>
    /// Gets an empty <see cref="OrderByCriteria"/>.
    /// </summary>
    public static OrderByCriteria Empty { get; } = new OrderByCriteria(ImmutableArray<OrderByCriterion>.Empty);

    /// <summary>
    /// Creates a new <see cref="OrderByCriteria"/> with the specified criterions.
    /// </summary>
    public static OrderByCriteria Create(ImmutableArray<OrderByCriterion> criterions) => new(criterions);

    /// <summary>
    /// Creates a mutable builder of immutable <see cref="OrderByCriteria"/> instances.
    /// </summary>
    public static Builder CreateBuilder() => new();

    /// <summary>
    /// The mutable builder for this immutable class.
    /// </summary>
    public sealed class Builder
    {
        internal Builder()
        {
        }

        /// <summary>
        /// The criterions that will form the criteria.
        /// </summary>
        public ImmutableArray<OrderByCriterion>.Builder Criterions { get; } = ImmutableArray.CreateBuilder<OrderByCriterion>();

        /// <summary>
        /// Creates an <see cref="OrderByCriteria"/> with the contents of this builder.
        /// </summary>
        public OrderByCriteria ToImmutable() => new(Criterions.ToImmutable());
    }
}