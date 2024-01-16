using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class ConvertCheckedWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = new ItemWireExpression();
        var expression = new ConvertCheckedWireExpression<int>(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("checked (System.Int32)(item)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = new ItemWireExpression();
        var result = WireExpression.ConvertChecked<int>(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ConvertCheckedWireExpression<int>>(result);
        Assert.Same(value, result.Expression);
        Assert.Same(typeof(int), result.Type);
    }
}
