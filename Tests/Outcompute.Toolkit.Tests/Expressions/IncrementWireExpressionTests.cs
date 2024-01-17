using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class IncrementWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = new ItemWireExpression();
        var expression = new IncrementWireExpression(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) + 1", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = new ItemWireExpression();
        var result = WireExpression.Increment(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<IncrementWireExpression>(result);
        Assert.Same(value, result.Expression);
    }
}
