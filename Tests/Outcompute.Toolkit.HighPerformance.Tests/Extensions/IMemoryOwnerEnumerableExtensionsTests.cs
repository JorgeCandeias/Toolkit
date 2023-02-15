namespace Outcompute.Toolkit.HighPerformance.Tests.Extensions;

public class IMemoryOwnerEnumerableExtensionsTests
{
    [Fact]
    public void AsEnumerableHandlesKnownMemoryOwner()
    {
        // arrange
        using var owner = (IMemoryOwner<int>)Enumerable.Range(1, 10).ToMemoryOwner();

        // act
        var result = owner.AsEnumerable();

        // assert
        Assert.Equal(owner.Memory.Span.ToArray(), result);
    }

    [Fact]
    public void AsEnumerableHandlesKnownArrayPoolBufferWriter()
    {
        // arrange
        using var owner = (IMemoryOwner<int>)Enumerable.Range(1, 10).ToArrayPoolBufferWriter();

        // act
        var result = owner.AsEnumerable();

        // assert
        Assert.Equal(owner.Memory.Span.ToArray(), result);
    }

    [Fact]
    public void AsEnumerableHandlesFallback()
    {
        // arrange
        using var owner = MemoryPool<int>.Shared.Rent(10);

        // act
        var result = owner.AsEnumerable();

        // assert
        Assert.Equal(owner.Memory.Span.ToArray(), result);
    }
}