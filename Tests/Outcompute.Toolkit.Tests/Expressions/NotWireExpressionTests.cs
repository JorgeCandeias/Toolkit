using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class NotWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = new ItemWireExpression();
        var expression = new NotWireExpression(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("!(item)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = new ItemWireExpression();
        var result = WireExpression.Not(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<NotWireExpression>(result);
        Assert.Same(value, result.Expression);
    }
}
