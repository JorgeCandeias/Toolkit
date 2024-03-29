﻿using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using Outcompute.Toolkit.HighPerformance.Extensions;

namespace Outcompute.Toolkit.HighPerformance.Tests.Extensions;

public static class MemoryOwnerEnumerableExtensionsTests
{
    [Fact]
    public static void ThrowsOnNullSource()
    {
        // arrange
        IEnumerable<int> source = null!;

        // act
        var ex = Assert.Throws<ArgumentNullException>(() => source.ToMemoryOwner());

        // assert
        Assert.Equal(nameof(source), ex.ParamName);
    }

    [Fact]
    public static void ConvertsEmptyArray()
    {
        // arrange
        var source = Array.Empty<int>();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Length, result.Length);
    }

    [Fact]
    public static void ConvertsFilledArray()
    {
        // arrange
        var source = Enumerable.Range(1, 100).ToArray();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Length, result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsEmptyList()
    {
        // arrange
        var source = new List<int>();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
    }

    [Fact]
    public static void ConvertsFilledList()
    {
        // arrange
        var source = Enumerable.Range(1, 100).ToList();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsEmptyQueue()
    {
        // arrange
        var source = new Queue<int>();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
    }

    [Fact]
    public static void ConvertsFilledQueue()
    {
        // arrange
        var source = new Queue<int>();
        foreach (var item in Enumerable.Range(1, 100))
        {
            source.Enqueue(item);
        }

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsEmptyStack()
    {
        // arrange
        var source = new Stack<int>();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
    }

    [Fact]
    public static void ConvertsFilledStack()
    {
        // arrange
        var source = new Stack<int>();
        foreach (var item in Enumerable.Range(1, 100))
        {
            source.Push(item);
        }

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsEmptyEnumerableWithKnownCount()
    {
        // arrange
        var source = new HashSet<int>();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
    }

    [Fact]
    public static void ConvertsFilledEnumerableWithKnownCount()
    {
        // arrange
        var source = Enumerable.Range(1, 100).ToHashSet();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count, result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsEnumerableWithUnknownCount()
    {
        // arrange
        static IEnumerable<int> Generate()
        {
            for (var i = 0; i < 1000; i++)
            {
                yield return i;
            }
        }
        var source = Generate();

        // act
        using var result = source.ToMemoryOwner();

        // act
        Assert.Equal(source.Count(), result.Length);
        Assert.Equal(source, result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsSpan()
    {
        // arrange
        var source = Enumerable.Range(1, 100).ToArray().AsSpan();

        // act
        using var result = source.ToMemoryOwner();

        // assert
        Assert.Equal(source.Length, result.Length);
        Assert.Equal(source.ToArray(), result.Span.ToArray());
    }

    [Fact]
    public static void ConvertsReadOnlySpan()
    {
        // arrange
        var source = (ReadOnlySpan<int>)Enumerable.Range(1, 100).ToArray().AsSpan();

        // act
        using var result = source.ToMemoryOwner();

        // assert
        Assert.Equal(source.Length, result.Length);
        Assert.Equal(source.ToArray(), result.Span.ToArray());
    }

    [Fact]
    public static void Enumerates()
    {
        // arrange
        var owner = MemoryOwner<int>.Allocate(1000);
        for (var i = 0; i < 1000; i++)
        {
            owner.Span[i] = i;
        }

        // act
        var result = owner.AsEnumerable().ToArray();

        // assert
        Assert.Equal(owner.Length, result.Length);
        Assert.Equal(owner.Span.ToArray(), result);
    }
}