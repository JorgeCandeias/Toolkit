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

    [Fact]
    public void FactoryCreatesExpression_And()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.And(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.And, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) & (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_AndAlso()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.AndAlso(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.AndAlso, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) && (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_AndAssign()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.AndAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.AndAssign, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) &= (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_Assign()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.Assign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.Assign, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) = (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_Coalesce()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.Coalesce(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.Coalesce, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) ?? (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_Divide()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.Divide(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.Divide, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) / (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_DivideAssign()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.DivideAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.DivideAssign, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) /= (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_Equal()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var lifted = true;
        var result = WireExpression.Equal(left, right, lifted);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.Equal, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.True(result.IsLiftedToNull);
        Assert.Equal("(item) == (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_ExclusiveOr()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.ExclusiveOr(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.ExclusiveOr, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) ^ (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_ExclusiveOrAssign()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.ExclusiveOrAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.ExclusiveOrAssign, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) ^= (default)", result.ToString());
    }
}