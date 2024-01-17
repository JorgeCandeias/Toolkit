using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Tests.Expressions;

public class BinaryWireExpressionTests
{
    [Fact]
    public void FactoryCreatesExpression_Add()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.Add(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.Add, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) + (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_AddAssign()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.AddAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.AddAssign, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) += (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_AddAssignChecked()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.AddAssignChecked(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.AddAssignChecked, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("checked ((item) += (default))", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_AddChecked()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.AddChecked(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.AddChecked, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("checked ((item) + (default))", result.ToString());
    }
}
