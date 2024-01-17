using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class NegateCheckedWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = new ItemWireExpression();
        var expression = new NegateCheckedWireExpression(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("checked -(item)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = new ItemWireExpression();
        var result = WireExpression.NegateChecked(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<NegateCheckedWireExpression>(result);
        Assert.Same(value, result.Expression);
    }
}
