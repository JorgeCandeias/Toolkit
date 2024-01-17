using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class MultiplyAssignCheckedWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var expression = new MultiplyAssignCheckedWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("checked ((item) *= (default))", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.MultiplyAssignChecked(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MultiplyAssignCheckedWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }
}
