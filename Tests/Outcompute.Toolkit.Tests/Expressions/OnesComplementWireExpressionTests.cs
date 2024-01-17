using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class OnesComplementWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = new ItemWireExpression();
        var expression = new OnesComplementWireExpression(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("~(item)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = new ItemWireExpression();
        var result = WireExpression.OnesComplement(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<OnesComplementWireExpression>(result);
        Assert.Same(value, result.Expression);
    }
}
