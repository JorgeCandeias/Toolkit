using Outcompute.Toolkit.Expressions;
using System.Collections.Immutable;

namespace Outcompute.Toolkit.Tests.Expressions;

public class ConditionalWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var test = new ItemWireExpression();
        var ifTrue = new DefaultWireExpression<int>();
        var ifFalse = new ArrayWireExpression<int>(ImmutableArray<int>.Empty);
        var expression = new ConditionalWireExpression(test, ifTrue, ifFalse);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) ? (default) : ([])", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var test = new ItemWireExpression();
        var ifTrue = new DefaultWireExpression<int>();
        var ifFalse = new ArrayWireExpression<int>(ImmutableArray<int>.Empty);
        var result = WireExpression.Condition(test, ifTrue, ifFalse);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ConditionalWireExpression>(result);
        Assert.Same(test, result.Test);
        Assert.Same(ifTrue, result.IfTrue);
        Assert.Same(ifFalse, result.IfFalse);
    }
}
