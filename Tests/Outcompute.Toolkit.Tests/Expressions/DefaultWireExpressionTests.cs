using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class DefaultWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var expression = new DefaultWireExpression();

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("default", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var result = WireExpression.Default();

        // assert
        Assert.NotNull(result);
        Assert.IsType<DefaultWireExpression>(result);
    }
}
