﻿using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class MultiplyAssignWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var expression = new MultiplyAssignWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) *= (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.MultiplyAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<MultiplyAssignWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }
}
