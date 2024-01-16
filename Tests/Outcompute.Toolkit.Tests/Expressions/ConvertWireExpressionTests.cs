using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class ConvertWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = new ItemWireExpression();
        var expression = new ConvertWireExpression<int>(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(System.Int32)(item)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = new ItemWireExpression();
        var result = WireExpression.Convert<int>(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ConvertWireExpression<int>>(result);
        Assert.Same(value, result.Expression);
        Assert.Same(typeof(int), result.Type);
    }
}
