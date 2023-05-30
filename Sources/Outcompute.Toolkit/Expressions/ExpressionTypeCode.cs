namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Represents the possible value types that an expression can evaluate to.
/// This is similar to the <see cref="Expression.Type"/> property in LINQ.
/// However, as <see cref="WireExpression"/> types are designed to be serializable, we cannot rely on a <see cref="Type"/> property.
/// Therefore, we must rely on something that resembles .NET's own <see cref="TypeCode"/> but specific to <see cref="WireExpression"/> types.
/// </summary>
public enum ExpressionTypeCode
{
    // default
    Void = 0,

    // well-known value types
    Boolean = 1001,

    Byte = 1002,
    SByte = 1003,
    Int16 = 1004,
    Int32 = 1005,
    Int64 = 1006,
    UInt16 = 1007,
    UInt32 = 1008,
    UInt64 = 1009,
    Single = 1010,
    Double = 1011,
    Decimal = 1012,
    TimeSpan = 1013,
    DateTime = 1014,
    Guid = 1015,
    Char = 1016,
    DateOnly = 1017,
    TimeOnly = 1018,
    Half = 1019,
    DateTimeOffset = 1020,

    // well-known nullable value types
    NullableBoolean = 2001,

    NullableByte = 2002,
    NullableSByte = 2003,
    NullableInt16 = 2004,
    NullableInt32 = 2005,
    NullableInt64 = 2006,
    NullableUInt16 = 2007,
    NullableUInt32 = 2008,
    NullableUInt64 = 2009,
    NullableSingle = 2010,
    NullableDouble = 2011,
    NullableDecimal = 2012,
    NullableTimeSpan = 2013,
    NullableDateTime = 2014,
    NullableGuid = 2015,
    NullableChar = 2016,
    NullableDateOnly = 2017,
    NullableTimeOnly = 2018,
    NullableHalf = 2019,
    NullableDateTimeOffset = 2020,

    // well-know primitive reference types
    String = 3001,

    // well-known collections,
    Array = 4001
}