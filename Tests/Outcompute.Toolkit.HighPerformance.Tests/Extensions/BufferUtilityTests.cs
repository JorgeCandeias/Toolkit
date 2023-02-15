namespace Outcompute.Toolkit.HighPerformance.Tests.Extensions;

public class BufferUtilityTests
{
    [Fact]
    public void GrowsBuffer()
    {
        // arrange
        var pool = ArrayPool<int>.Shared;
        var buffer = pool.Rent(10);
        var length = buffer.Length;
        var data = Enumerable.Range(1, 10);
        data.CopyTo(buffer, 0);

        // act
        BufferUtility.Shared.Grow(pool, ref buffer, 2);

        // assert
        Assert.Equal(length * 2, buffer.Length);
        Assert.Equal(data, buffer[..10]);
    }

    [Fact]
    public void ThrowsOnInsuffientMemory()
    {
        // arrange
        var pool = ArrayPool<int>.Shared;
        var buffer = pool.Rent(10);
        var data = Enumerable.Range(1, 10);
        data.CopyTo(buffer, 0);

        // act + assert
        var utility = new BufferUtility(16);
        Assert.Throws<InsufficientMemoryException>(() => utility.Grow(pool, ref buffer, 2));
    }
}