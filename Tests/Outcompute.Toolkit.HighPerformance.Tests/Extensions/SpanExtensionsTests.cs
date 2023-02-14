using CommunityToolkit.HighPerformance;
using Outcompute.Toolkit.HighPerformance.Extensions;
using System.Text;

namespace Outcompute.Toolkit.HighPerformance.Tests.Extensions;

public class SpanExtensionsTests
{
    [Fact]
    public void ConvertsReadOnlySpanChar()
    {
        // arrange
        var array = new char[] { 'A', 'B', 'C' };
        ReadOnlySpan<char> span = array.AsSpan();

        // act
        var result = span.ToPooledString();

        // assert
        Assert.Equal("ABC", result);
    }

    [Fact]
    public void ConvertsSpanChar()
    {
        // arrange
        var array = new char[] { 'A', 'B', 'C' };
        Span<char> span = array.AsSpan();

        // act
        var result = span.ToPooledString();

        // assert
        Assert.Equal("ABC", result);
    }

    [Fact]
    public void ConvertsReadOnlySpanByte()
    {
        // arrange
        var encoding = Encoding.UTF8;
        var array = encoding.GetBytes("ABC");
        ReadOnlySpan<byte> span = array.AsSpan();

        // act
        var result = span.ToPooledString(encoding);

        // assert
        Assert.Equal("ABC", result);
    }

    [Fact]
    public void ConvertsSpanByte()
    {
        // arrange
        var encoding = Encoding.UTF8;
        var array = encoding.GetBytes("ABC");
        Span<byte> span = array.AsSpan();

        // act
        var result = span.ToPooledString(encoding);

        // assert
        Assert.Equal("ABC", result);
    }
}