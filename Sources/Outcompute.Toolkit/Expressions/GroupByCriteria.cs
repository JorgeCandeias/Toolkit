using System.Collections.Immutable;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Convenience holder, builder and validation of a complete grouping criteria.
/// </summary>
public class GroupByCriteria
{
    public GroupByCriteria(ImmutableArray<WireExpression> expressions)
    {
        if (expressions.IsDefaultOrEmpty)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(expressions), "At least one expression is required");
        }

        for (var i = 0; i < expressions.Length; i++)
        {
            if (expressions[i] is null)
            {
                ThrowHelper.ThrowArgumentException(nameof(expressions), $"{nameof(expressions)}[{i}] cannot be null");
            }
        }

        Expressions = expressions;
    }

    /// <summary>
    /// The set of expressions that make up this criteria.
    /// </summary>
    public ImmutableArray<WireExpression> Expressions { get; }

    /// <summary>
    /// Indicates if this criteria is empty.
    /// </summary>
    public bool IsEmpty => Expressions.IsEmpty;

    /// <summary>
    /// Gets a new <see cref="GroupByCriteria"/> with all the current expressions plus the specified criterion appended.
    /// </summary>
    public GroupByCriteria ThenBy(WireExpression expression)
    {
        Guard.IsNotNull(expression);

        return new(Expressions.Add(expression));
    }

    /// <summary>
    /// Creates a new <see cref="GroupByCriteria"/> from the specified expressions.
    /// </summary>
    public static GroupByCriteria Create(ImmutableArray<WireExpression> expressions) => new(expressions);

    /// <summary>
    /// The mutable builder for the <see cref="GroupByCriteria"/> immutable class.
    /// </summary>
    public class Builder
    {
        internal Builder()
        {
        }

        /// <summary>
        /// The set of expressions that will make up the criteria.
        /// </summary>
        public ImmutableArray<WireExpression>.Builder Expressions { get; } = ImmutableArray.CreateBuilder<WireExpression>();

        /// <summary>
        /// Creates a new <see cref="GroupByCriteria"/> with the current contents of this builder.
        /// </summary>
        public GroupByCriteria ToImmutable() => new(Expressions.ToImmutable());
    }
}