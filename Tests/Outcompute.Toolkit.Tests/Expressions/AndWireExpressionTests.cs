using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class AndWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var expression = new AndWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) & (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.And(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<AndWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }

    [Fact]
    public void FactoryCreatesCompositeExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.And(new WireExpression[] { left, right });

        // assert
        Assert.NotNull(result);
        var typed = Assert.IsType<AndWireExpression>(result);
        Assert.Same(left, typed.Left);
        Assert.Same(right, typed.Right);
    }

    [Fact]
    public void FactoryCreatesNullExpression()
    {
        // act
        var result = WireExpression.And(Array.Empty<WireExpression>());

        // assert
        Assert.Null(result);
    }
}
