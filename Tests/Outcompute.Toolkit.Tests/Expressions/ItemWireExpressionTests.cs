using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class ItemWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var expression = new ItemWireExpression();

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("item", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var result = WireExpression.Item();

        // assert
        Assert.NotNull(result);
        Assert.IsType<ItemWireExpression>(result);
    }
}
