using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class AssignWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var expression = new AssignWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) = (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var target = new ItemWireExpression();
        var value = new DefaultWireExpression<int>();
        var result = WireExpression.Assign(target, value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<AssignWireExpression>(result);
        Assert.Same(target, result.Target);
        Assert.Same(value, result.Value);
    }
}
