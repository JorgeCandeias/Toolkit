using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class DivideWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var expression = new DivideWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) / (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.Divide(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<DivideWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }
}
