using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class AddWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var expression = new AddWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) + (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var result = WireExpression.Add(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<AddWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }
}
