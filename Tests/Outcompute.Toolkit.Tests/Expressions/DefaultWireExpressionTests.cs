﻿using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class DefaultWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var expression = new DefaultWireExpression<int>();

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("default", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var result = WireExpression.Default<int>();

        // assert
        Assert.NotNull(result);
        Assert.IsType<DefaultWireExpression<int>>(result);
        Assert.Same(typeof(int), result.Type);
    }
}
