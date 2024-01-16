using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class DecrementWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = new ItemWireExpression();
        var expression = new DecrementWireExpression(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) - 1", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = new ItemWireExpression();
        var result = WireExpression.Decrement(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<DecrementWireExpression>(result);
        Assert.Same(value, result.Expression);
    }
}
