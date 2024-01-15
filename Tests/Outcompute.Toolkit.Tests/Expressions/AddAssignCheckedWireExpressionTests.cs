using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class AddAssignCheckedWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var expression = new AddAssignCheckedWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("checked ((item) += (default))", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var result = WireExpression.AddAssignChecked(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<AddAssignCheckedWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }
}
