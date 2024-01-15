using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class WireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var expression = Mock.Of<WireExpression>();
        Mock.Get(expression).CallBase = true;

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("", result);
    }
}
