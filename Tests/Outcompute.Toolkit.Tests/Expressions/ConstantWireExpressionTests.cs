using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class ConstantWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = 1;
        var expression = new ConstantWireExpression<int>(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(1)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = 1;
        var result = WireExpression.Constant(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ConstantWireExpression<int>>(result);
        Assert.Equal(value, result.Value);
    }
}
