﻿using Outcompute.Toolkit.Extensions;
using System.Buffers;
using System.Globalization;

namespace Outcompute.Toolkit.Expressions.Visitors;

/// <summary>
/// Debugging implementation of <see cref="WireExpressionVisitor"/>.
/// This visitor renders a string representation of a <see cref="WireExpression"/> for use with IDE debugging features.
/// </summary>
internal sealed class StringWireExpressionVisitor : WireExpressionVisitor, IDisposable
{
    private readonly ArrayPoolBufferWriter<char> _buffer = new();

    public override string ToString() => _buffer.ToString();

    public void Dispose() => _buffer.Dispose();

    private void Write(ReadOnlySpan<char> value) => _buffer.Write(value);

    private void WriteQuoted<TValue>(TValue value)
    {
        switch (value)
        {
            case null:
                Write("<NULL>");
                break;

            case string str:
                Write("\"");
                Write(str.Replace("\"", "\"\""));
                Write("\"");
                break;

            case char c:
                Write("'");
                Write(c.ToString());
                Write("'");
                break;

            case IFormattable formattable:
                Write(formattable.ToString(null, CultureInfo.InvariantCulture));
                break;

            default:
                Write(value.ToString());
                break;
        }
    }

    protected internal override WireExpression VisitItem(ItemWireExpression expression)
    {
        Write("item");

        return expression;
    }

    protected internal override WireExpression VisitAdd(AddWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") + (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitAddAssign(AddAssignWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") += (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitAddAssignChecked(AddAssignCheckedWireExpression expression)
    {
        Write("checked ((");
        Visit(expression.Left);
        Write(") += (");
        Visit(expression.Right);
        Write("))");

        return expression;
    }

    protected internal override WireExpression VisitAddChecked(AddCheckedWireExpression expression)
    {
        Write("checked ((");
        Visit(expression.Left);
        Write(") + (");
        Visit(expression.Right);
        Write("))");

        return expression;
    }

    protected internal override WireExpression VisitAnd(AndWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") & (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitAndAlso(AndAlsoWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") && (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitAndAssign(AndAssignWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") &= (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitArray<TValue>(ArrayWireExpression<TValue> expression)
    {
        Write("[");

        var enumerator = expression.Values.GetEnumerator();
        if (enumerator.MoveNext())
        {
            WriteQuoted(enumerator.Current);

            while (enumerator.MoveNext())
            {
                Write(", ");
                WriteQuoted(enumerator.Current);
            }
        }

        Write("]");

        return expression;
    }

    protected internal override WireExpression VisitAssign(AssignWireExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(") = (");
        Visit(expression.Value);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitCoalesce(CoalesceWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") ?? (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitCondition(ConditionalWireExpression expression)
    {
        Write("(");
        Visit(expression.Test);
        Write(") ? (");
        Visit(expression.IfTrue);
        Write(") : (");
        Visit(expression.IfFalse);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitConstant<T>(ConstantWireExpression<T> expression)
    {
        if (expression.Value is null)
        {
            Write("null");
        }
        else
        {
            Write("(");
            WriteQuoted(expression.Value);
            Write(")");
        }

        return expression;
    }

    protected internal override WireExpression VisitConvert<TValue>(ConvertWireExpression<TValue> expression)
    {
        Write("(");
        Write(expression.Type.FullName);
        Write(")(");
        Visit(expression.Expression);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitConvertChecked<TValue>(ConvertCheckedWireExpression<TValue> expression)
    {
        Write("checked (");
        Write(expression.Type.FullName);
        Write(")(");
        Visit(expression.Expression);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitDecrement(DecrementWireExpression expression)
    {
        Write("(");
        Visit(expression.Expression);
        Write(") - 1");

        return expression;
    }

    protected internal override WireExpression VisitDefault<TValue>(DefaultWireExpression<TValue> expression)
    {
        Write("default");

        return expression;
    }

    protected internal override WireExpression VisitDivide(DivideWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") / (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitDivideAssign(DivideAssignWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") /= (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitEmpty(EmptyWireExpression expression)
    {
        Write("{}");

        return expression;
    }

    protected internal override WireExpression VisitEqual(EqualWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") == (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitExclusiveOr(ExclusiveOrWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") ^ (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitExclusiveOrAssign(ExclusiveOrAssignWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") ^= (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitField(FieldWireExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(").");
        Write(expression.Name);

        return expression;
    }

    protected internal override WireExpression VisitGreaterThan(GreaterThanWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") > (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitGreaterThanOrEqual(GreaterThanOrEqualWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") >= (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitIncrement(IncrementWireExpression expression)
    {
        Write("(");
        Visit(expression.Expression);
        Write(") + 1");

        return expression;
    }

    protected internal override WireExpression VisitIsFalse(IsFalseWireExpression expression)
    {
        Write("(");
        Visit(expression.Expression);
        Write(") == false");

        return expression;
    }

    protected internal override WireExpression VisitIsTrue(IsTrueWireExpression expression)
    {
        Write("(");
        Visit(expression.Expression);
        Write(") == true");

        return expression;
    }














    protected internal override WireExpression VisitProperty(PropertyExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(").");
        Write(expression.Name);

        return expression;
    }

    protected internal override WireExpression VisitPropertyOrField(PropertyOrFieldExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(").");
        Write(expression.Name);

        return expression;
    }

    protected internal override WireExpression VisitNot(NotExpression expression)
    {
        Write("!(");
        Visit(expression.Target);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitIsNull(IsNullExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(") == null");

        return expression;
    }

    protected internal override WireExpression VisitIsNotNull(IsNotNullExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(") != null");

        return expression;
    }

    protected internal override WireExpression VisitNotEqual(NotEqualWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") != (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitOr(OrExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") | (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitOrElse(OrElseExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") || (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitLessThan(LessThanWireExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") < (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitLessThanOrEqual(LessThanOrEqualExpression expression)
    {
        Write("(");
        Visit(expression.Left);
        Write(") <= (");
        Visit(expression.Right);
        Write(")");

        return expression;
    }


    protected internal override WireExpression VisitStringContains(StringContainsExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(").Contains(");
        Visit(expression.Value);
        Write(", ");
        Write(nameof(StringComparison));
        Write(".");
        Write(expression.Comparison.AsString());
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitStringCompare(StringCompareExpression expression)
    {
        Write("string.Compare(");
        Visit(expression.Target);
        Write(", ");
        Visit(expression.Value);
        Write(", ");
        Write(nameof(StringComparison));
        Write(".");
        Write(expression.Comparison.AsString());
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitStringStartsWith(StringStartsWithExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(").StartsWith(");
        Visit(expression.Value);
        Write(", ");
        Write(nameof(StringComparison));
        Write(".");
        Write(expression.Comparison.AsString());
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitStringEndsWith(StringEndsWithExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(").EndsWith(");
        Visit(expression.Value);
        Write(", ");
        Write(nameof(StringComparison));
        Write(".");
        Write(expression.Comparison.AsString());
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitStringIsNullOrWhiteSpace(StringIsNullOrWhiteSpaceExpression expression)
    {
        Write("string.IsNullOrWhiteSpace(");
        Visit(expression.Target);
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitStringEqual(StringEqualExpression expression)
    {
        Write("string.Equals(");
        Visit(expression.Target);
        Write(", ");
        Visit(expression.Value);
        Write(", ");
        Write(nameof(StringComparison));
        Write(".");
        Write(expression.Comparison.AsString());
        Write(")");

        return expression;
    }

    protected internal override WireExpression VisitHashSet<TValue>(HashSetExpression<TValue> expression)
    {
        Write("[");

        var enumerator = expression.Values.GetEnumerator();
        if (enumerator.MoveNext())
        {
            WriteQuoted(enumerator.Current);

            while (enumerator.MoveNext())
            {
                Write(", ");
                WriteQuoted(enumerator.Current);
            }
        }

        Write("]");

        return expression;
    }

    protected internal override WireExpression VisitContains(ContainsExpression expression)
    {
        Write("(");
        Visit(expression.Target);
        Write(").Contains(");
        Visit(expression.Value);
        Write(")");

        return expression;
    }
}