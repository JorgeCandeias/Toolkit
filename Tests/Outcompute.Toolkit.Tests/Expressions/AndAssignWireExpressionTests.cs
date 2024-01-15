﻿using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class AndAssignWireExpressionTests
{
    [Fact]
    public void ToStringEmitsText()
    {
        // arrange
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var expression = new AndAssignWireExpression(left, right);

        // act
        var result = expression.ToString();

        // assert
        Assert.Equal("(item) &= (default)", result);
    }

    [Fact]
    public void FactoryCreatesExpression()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression();
        var result = WireExpression.AndAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.IsType<AndAssignWireExpression>(result);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
    }
}
