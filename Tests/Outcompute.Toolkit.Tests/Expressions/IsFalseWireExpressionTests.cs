using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class IsFalseWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = new ItemWireExpression();
        var expression = new IsFalseWireExpression(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) == false", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = new ItemWireExpression();
        var result = WireExpression.IsFalse(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<IsFalseWireExpression>(result);
        Assert.Same(value, result.Expression);
    }
}
