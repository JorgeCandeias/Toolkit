using Outcompute.Toolkit.Extensions;

namespace Outcompute.Toolkit.Tests.Extensions;

public class StructExtensionsTests
{
    [Fact]
    public void AsNullableGetsNullable()
    {
        // arrange
        var value = 0;

        // act
        var result = value.AsNullable();

        // assert
        Assert.True(result.HasValue);
        Assert.Equal(value, result.Value);
    }
}