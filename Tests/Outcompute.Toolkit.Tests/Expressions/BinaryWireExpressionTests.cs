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

    [Fact]
    public void FactoryCreatesExpression_GreaterThan()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var lifted = true;
        var result = WireExpression.GreaterThan(left, right, lifted);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.GreaterThan, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal(lifted, result.IsLiftedToNull);
        Assert.Equal("(item) > (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_GreaterThanOrEqual()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var lifted = true;
        var result = WireExpression.GreaterThanOrEqual(left, right, lifted);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.GreaterThanOrEqual, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal(lifted, result.IsLiftedToNull);
        Assert.Equal("(item) >= (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_LessThan()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var lifted = true;
        var result = WireExpression.LessThan(left, right, true);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.LessThan, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal(lifted, result.IsLiftedToNull);
        Assert.Equal("(item) < (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_LessThanOrEqual()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var lifted = true;
        var result = WireExpression.LessThanOrEqual(left, right, lifted);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.LessThanOrEqual, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal(lifted, result.IsLiftedToNull);
        Assert.Equal("(item) <= (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_Modulo()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.Modulo(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.Modulo, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) % (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_ModuloAssign()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.ModuloAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.ModuloAssign, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) %= (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_Multiply()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.Multiply(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.Multiply, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) * (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_MultiplyChecked()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.MultiplyChecked(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.MultiplyChecked, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("checked ((item) * (default))", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_MultiplyAssign()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.MultiplyAssign(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.MultiplyAssign, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) *= (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_MultiplyAssignChecked()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.MultiplyAssignChecked(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.MultiplyAssignChecked, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("checked ((item) *= (default))", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_NotEqual()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var lifted = true;
        var result = WireExpression.NotEqual(left, right, lifted);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.NotEqual, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal(lifted, result.IsLiftedToNull);
        Assert.Equal("(item) != (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_Or()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.Or(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.Or, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) | (default)", result.ToString());
    }

    [Fact]
    public void FactoryCreatesExpression_OrElse()
    {
        // act
        var left = new ItemWireExpression();
        var right = new DefaultWireExpression<int>();
        var result = WireExpression.OrElse(left, right);

        // assert
        Assert.NotNull(result);
        Assert.Equal(BinaryWireOperation.OrElse, result.Operation);
        Assert.Same(left, result.Left);
        Assert.Same(right, result.Right);
        Assert.Equal("(item) || (default)", result.ToString());
    }
}