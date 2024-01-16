using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]

// standard expressions
[ProtoInclude(2, typeof(ItemExpressionSurrogate))]
[ProtoInclude(3, typeof(PropertyExpressionSurrogate))]
[ProtoInclude(4, typeof(FieldExpressionSurrogate))]
[ProtoInclude(5, typeof(PropertyOrFieldExpressionSurrogate))]
[ProtoInclude(6, typeof(NotExpressionSurrogate))]
[ProtoInclude(7, typeof(IsNullExpressionSurrogate))]
[ProtoInclude(8, typeof(IsNotNullExpressionSurrogate))]
[ProtoInclude(9, typeof(EqualExpressionSurrogate))]
[ProtoInclude(10, typeof(NotEqualExpressionSurrogate))]
[ProtoInclude(11, typeof(AndExpressionSurrogate))]
[ProtoInclude(12, typeof(AndAlsoExpressionSurrogate))]
[ProtoInclude(13, typeof(OrExpressionSurrogate))]
[ProtoInclude(14, typeof(OrElseExpressionSurrogate))]
[ProtoInclude(15, typeof(LessThanExpressionSurrogate))]
[ProtoInclude(16, typeof(LessThanOrEqualExpressionSurrogate))]
[ProtoInclude(17, typeof(GreaterThanExpressionSurrogate))]
[ProtoInclude(18, typeof(GreaterThanOrEqualExpressionSurrogate))]
[ProtoInclude(19, typeof(AddExpressionSurrogate))]
[ProtoInclude(20, typeof(StringContainsExpressionSurrogate))]
[ProtoInclude(21, typeof(StringCompareExpressionSurrogate))]
[ProtoInclude(22, typeof(StringStartsWithExpressionSurrogate))]
[ProtoInclude(23, typeof(StringEndsWithExpressionSurrogate))]
[ProtoInclude(24, typeof(StringIsNullOrWhiteSpaceExpressionSurrogate))]
[ProtoInclude(25, typeof(StringEqualExpressionSurrogate))]
[ProtoInclude(26, typeof(AssignExpressionSurrogate))]
[ProtoInclude(27, typeof(ContainsExpressionSurrogate))]

// constant expressions - well-known non-nullable value types */
[ProtoInclude(1001, typeof(ConstantExpressionSurrogate<bool>))]
[ProtoInclude(1002, typeof(ConstantExpressionSurrogate<byte>))]
[ProtoInclude(1003, typeof(ConstantExpressionSurrogate<sbyte>))]
[ProtoInclude(1004, typeof(ConstantExpressionSurrogate<short>))]
[ProtoInclude(1005, typeof(ConstantExpressionSurrogate<ushort>))]
[ProtoInclude(1006, typeof(ConstantExpressionSurrogate<int>))]
[ProtoInclude(1007, typeof(ConstantExpressionSurrogate<uint>))]
[ProtoInclude(1008, typeof(ConstantExpressionSurrogate<long>))]
[ProtoInclude(1009, typeof(ConstantExpressionSurrogate<ulong>))]
[ProtoInclude(1010, typeof(ConstantExpressionSurrogate<float>))]
[ProtoInclude(1011, typeof(ConstantExpressionSurrogate<double>))]
[ProtoInclude(1012, typeof(ConstantExpressionSurrogate<decimal>))]
[ProtoInclude(1012, typeof(ConstantExpressionSurrogate<char>))]
[ProtoInclude(1013, typeof(ConstantExpressionSurrogate<TimeSpan>))]
[ProtoInclude(1014, typeof(ConstantExpressionSurrogate<DateTime>))]
[ProtoInclude(1015, typeof(ConstantExpressionSurrogate<Guid>))]
[ProtoInclude(1016, typeof(ConstantExpressionSurrogate<Half>))]
[ProtoInclude(1017, typeof(ConstantExpressionSurrogate<DateOnly>))]
[ProtoInclude(1018, typeof(ConstantExpressionSurrogate<TimeOnly>))]
[ProtoInclude(1019, typeof(ConstantExpressionSurrogate<DateTimeOffset>))]

// constant expressions - well-known nullable value types */
[ProtoInclude(2001, typeof(ConstantExpressionSurrogate<bool?>))]
[ProtoInclude(2002, typeof(ConstantExpressionSurrogate<byte?>))]
[ProtoInclude(2003, typeof(ConstantExpressionSurrogate<sbyte?>))]
[ProtoInclude(2004, typeof(ConstantExpressionSurrogate<short?>))]
[ProtoInclude(2005, typeof(ConstantExpressionSurrogate<ushort?>))]
[ProtoInclude(2006, typeof(ConstantExpressionSurrogate<int?>))]
[ProtoInclude(2007, typeof(ConstantExpressionSurrogate<uint?>))]
[ProtoInclude(2008, typeof(ConstantExpressionSurrogate<long?>))]
[ProtoInclude(2009, typeof(ConstantExpressionSurrogate<ulong?>))]
[ProtoInclude(2010, typeof(ConstantExpressionSurrogate<float?>))]
[ProtoInclude(2011, typeof(ConstantExpressionSurrogate<double?>))]
[ProtoInclude(2012, typeof(ConstantExpressionSurrogate<decimal?>))]
[ProtoInclude(2012, typeof(ConstantExpressionSurrogate<char?>))]
[ProtoInclude(2013, typeof(ConstantExpressionSurrogate<TimeSpan?>))]
[ProtoInclude(2014, typeof(ConstantExpressionSurrogate<DateTime?>))]
[ProtoInclude(2015, typeof(ConstantExpressionSurrogate<Guid?>))]
[ProtoInclude(2016, typeof(ConstantExpressionSurrogate<Half?>))]
[ProtoInclude(2017, typeof(ConstantExpressionSurrogate<DateOnly?>))]
[ProtoInclude(2018, typeof(ConstantExpressionSurrogate<TimeOnly?>))]
[ProtoInclude(2019, typeof(ConstantExpressionSurrogate<DateTimeOffset?>))]

// constant expressions - well-known reference types */
[ProtoInclude(3001, typeof(ConstantExpressionSurrogate<string>))]

// hashset expressions - well-known non-nullable value types */
[ProtoInclude(4001, typeof(HashSetExpressionSurrogate<bool>))]
[ProtoInclude(4002, typeof(HashSetExpressionSurrogate<byte>))]
[ProtoInclude(4003, typeof(HashSetExpressionSurrogate<sbyte>))]
[ProtoInclude(4004, typeof(HashSetExpressionSurrogate<short>))]
[ProtoInclude(4005, typeof(HashSetExpressionSurrogate<ushort>))]
[ProtoInclude(4006, typeof(HashSetExpressionSurrogate<int>))]
[ProtoInclude(4007, typeof(HashSetExpressionSurrogate<uint>))]
[ProtoInclude(4008, typeof(HashSetExpressionSurrogate<long>))]
[ProtoInclude(4009, typeof(HashSetExpressionSurrogate<ulong>))]
[ProtoInclude(4010, typeof(HashSetExpressionSurrogate<float>))]
[ProtoInclude(4011, typeof(HashSetExpressionSurrogate<double>))]
[ProtoInclude(4012, typeof(HashSetExpressionSurrogate<decimal>))]
[ProtoInclude(4012, typeof(HashSetExpressionSurrogate<char>))]
[ProtoInclude(4013, typeof(HashSetExpressionSurrogate<TimeSpan>))]
[ProtoInclude(4014, typeof(HashSetExpressionSurrogate<DateTime>))]
[ProtoInclude(4015, typeof(HashSetExpressionSurrogate<Guid>))]
[ProtoInclude(4016, typeof(HashSetExpressionSurrogate<Half>))]
[ProtoInclude(4017, typeof(HashSetExpressionSurrogate<DateOnly>))]
[ProtoInclude(4018, typeof(HashSetExpressionSurrogate<TimeOnly>))]
[ProtoInclude(4019, typeof(HashSetExpressionSurrogate<DateTimeOffset>))]

// hashset expressions - well-known nullable value types */
[ProtoInclude(5001, typeof(HashSetExpressionSurrogate<bool?>))]
[ProtoInclude(5002, typeof(HashSetExpressionSurrogate<byte?>))]
[ProtoInclude(5003, typeof(HashSetExpressionSurrogate<sbyte?>))]
[ProtoInclude(5004, typeof(HashSetExpressionSurrogate<short?>))]
[ProtoInclude(5005, typeof(HashSetExpressionSurrogate<ushort?>))]
[ProtoInclude(5006, typeof(HashSetExpressionSurrogate<int?>))]
[ProtoInclude(5007, typeof(HashSetExpressionSurrogate<uint?>))]
[ProtoInclude(5008, typeof(HashSetExpressionSurrogate<long?>))]
[ProtoInclude(5009, typeof(HashSetExpressionSurrogate<ulong?>))]
[ProtoInclude(5010, typeof(HashSetExpressionSurrogate<float?>))]
[ProtoInclude(5011, typeof(HashSetExpressionSurrogate<double?>))]
[ProtoInclude(5012, typeof(HashSetExpressionSurrogate<decimal?>))]
[ProtoInclude(5012, typeof(HashSetExpressionSurrogate<char?>))]
[ProtoInclude(5013, typeof(HashSetExpressionSurrogate<TimeSpan?>))]
[ProtoInclude(5014, typeof(HashSetExpressionSurrogate<DateTime?>))]
[ProtoInclude(5015, typeof(HashSetExpressionSurrogate<Guid?>))]
[ProtoInclude(5016, typeof(HashSetExpressionSurrogate<Half?>))]
[ProtoInclude(5017, typeof(HashSetExpressionSurrogate<DateOnly?>))]
[ProtoInclude(5018, typeof(HashSetExpressionSurrogate<TimeOnly?>))]
[ProtoInclude(5019, typeof(HashSetExpressionSurrogate<DateTimeOffset?>))]

// constant expressions - well-known reference types */
[ProtoInclude(6001, typeof(HashSetExpressionSurrogate<string>))]

// type ddefault expressions
[ProtoInclude(7001, typeof(DefaultExpressionSurrogate<int>))]

internal abstract record class QueryExpressionSurrogate
{
    /// <summary>
    /// Derived expressions implement this method to redirect the call to the correct visitor method.
    /// </summary>
    protected internal abstract QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor);

    /// <summary>
    /// Converts the specified <paramref name="expression"/> into a <see cref="QueryExpressionSurrogate"/> via the appropriate visitor.
    /// </summary>
    public static implicit operator QueryExpressionSurrogate(WireExpression expression)
    {
        var visitor = new ProtobufQueryExpressionVisitor();

        visitor.Visit(expression);

        return visitor.Result;
    }

    /// <summary>
    /// Converts the specified <paramref name="surrogate"/> into a <see cref="WireExpression"/> via the appropriate visitor.
    /// </summary>
    public static implicit operator WireExpression(QueryExpressionSurrogate surrogate)
    {
        var visitor = new ProtobufQueryExpressionSurrogateVisitor();

        return visitor.Visit(surrogate);
    }
}