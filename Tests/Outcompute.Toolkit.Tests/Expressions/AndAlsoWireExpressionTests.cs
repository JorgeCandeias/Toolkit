using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class AndAlsoWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var expression = new AndAlsoWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) && (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var result = WireExpression.AndAlso(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<AndAlsoWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }

    [Fact]
    public void FactoryCreatesCompositeExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var result = WireExpression.AndAlso(new WireExpression[] { left, right });

        // assert
        Assert.NotNull(result);
        var typed = Assert.IsType<AndAlsoWireExpression>(result);
        Assert.Same(left, typed.Left);
        Assert.Same(right, typed.Right);
    }

    [Fact]
    public void FactoryCreatesNullExpression()
    {
        // act
        var result = WireExpression.AndAlso(Array.Empty<WireExpression>());

        // assert
        Assert.Null(result);
    }
}
