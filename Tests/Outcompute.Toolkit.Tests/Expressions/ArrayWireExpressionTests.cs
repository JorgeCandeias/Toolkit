using Outcompute.Toolkit.Expressions;
using System.Collections.Immutable;

namespace Outcompute.Toolkit.Tests.Expressions;

public class ArrayWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var values = ImmutableArray.Create(1, 2, 3);
        var expression = new ArrayWireExpression<int>(values);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("[1, 2, 3]", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var values = ImmutableArray.Create(1, 2, 3);
        var result = WireExpression.Array(values);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ArrayWireExpression<int>>(result);
        Assert.Equal(values, result.Values);
    }
}
