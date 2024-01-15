using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class AddAssignWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var expression = new AddAssignWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) += (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var result = WireExpression.AddAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<AddAssignWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }
}
