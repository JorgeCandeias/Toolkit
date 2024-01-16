using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class DivideAssignWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var expression = new DivideAssignWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) /= (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.DivideAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<DivideAssignWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }
}
