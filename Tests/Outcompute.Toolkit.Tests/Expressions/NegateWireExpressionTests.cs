﻿using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class NegateWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var value = new ItemWireExpression();
        var expression = new NegateWireExpression(value);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("-(item)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var value = new ItemWireExpression();
        var result = WireExpression.Negate(value);

        // assert
        Assert.NotNull(result);
        Assert.IsType<NegateWireExpression>(result);
        Assert.Same(value, result.Expression);
    }
}
