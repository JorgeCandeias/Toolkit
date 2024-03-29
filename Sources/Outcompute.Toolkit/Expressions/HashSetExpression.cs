﻿using Outcompute.Toolkit.Expressions.Visitors;
using System.Collections.Immutable;

namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Represents a HashSet.
/// </summary>
public sealed record class HashSetExpression<T> : WireExpression
{
    internal HashSetExpression(ImmutableHashSet<T> value)
    {
        Values = value;
    }

    /// <summary>
    /// The constant value.
    /// </summary>
    public ImmutableHashSet<T> Values { get; }

    /// <summary>
    /// Makes the specified visitor visit the current expression using the correct overload.
    /// </summary>
    protected internal override WireExpression Accept(WireExpressionVisitor visitor) => visitor.VisitHashSet(this);
}

public partial record class WireExpression
{
    /// <summary>
    /// Creates a new <see cref="HashSetExpression{T}"/> with the specified parameters.
    /// </summary>
    public static HashSetExpression<T> HashSet<T>(ImmutableHashSet<T> value) => new(value);
}