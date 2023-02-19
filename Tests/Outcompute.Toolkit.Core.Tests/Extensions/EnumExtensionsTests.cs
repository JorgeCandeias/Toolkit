namespace Outcompute.Toolkit.Core.Tests.Extensions;

public partial class EnumExtensionsTests
{
    [Fact]
    public void EnumAsStringReturnsValue()
    {
        // arrange
        var value = TaskStatus.Running;

        // act
        var result = value.AsString();

        // assert
        Assert.Equal(nameof(TaskStatus.Running), result);
    }
}