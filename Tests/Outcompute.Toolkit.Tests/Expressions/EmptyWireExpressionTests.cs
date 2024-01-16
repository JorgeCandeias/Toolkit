using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class EmptyWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var expression = new EmptyWireExpression();

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("{}", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var result = WireExpression.Empty();

        // assert
        Assert.NotNull(result);
        Assert.IsType<EmptyWireExpression>(result);
    }
}
