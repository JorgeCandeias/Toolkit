using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class FieldWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var target = new ItemWireExpression();
        var name = "Property";
        var expression = new FieldWireExpression(target, name);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item).Property", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var target = new ItemWireExpression();
        var name = "Property";
        var result = WireExpression.Field(target, name);

        // assert
        Assert.NotNull(result);
        Assert.IsType<FieldWireExpression>(result);
        Assert.Same(target, result.Target);
        Assert.Same(name, result.Name);
    }
}
