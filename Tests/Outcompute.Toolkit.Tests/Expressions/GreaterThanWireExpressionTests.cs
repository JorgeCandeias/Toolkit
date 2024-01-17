using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class GreaterThanWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var expression = new GreaterThanWireExpression(left, right, true);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) > (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.GreaterThan(left, right, true);

        // assert
        Assert.NotNull(result);
        Assert.IsType<GreaterThanWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.True(result.IsLiftedToNull);
    }
}
