using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class MultiplyCheckedWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var expression = new MultiplyCheckedWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("checked ((item) * (default))", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.MultiplyChecked(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MultiplyCheckedWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }
}
