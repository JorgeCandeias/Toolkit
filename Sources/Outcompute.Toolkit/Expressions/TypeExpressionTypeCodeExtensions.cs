namespace Outcompute.Toolkit.Expressions;

/// <summary>
/// Holds extensions to allow discovering the appropriate <see cref="ExpressionTypeCode"/> for a <see cref="Type"/>.
/// </summary>
public static class TypeExpressionTypeCodeExtensions
{
    /// <summary>
    /// Maps supported well-known <see cref="Type"/>s to their corresponding <see cref="ExpressionTypeCode"/> enum members.
    /// </summary>
    private static readonly Dictionary<Type, ExpressionTypeCode> ForwardLookup = new()
    {
        // default
        { typeof(void), ExpressionTypeCode.Void },

        // well-know value types
        { typeof(bool), ExpressionTypeCode.Boolean },
        { typeof(byte), ExpressionTypeCode.Byte },
        { typeof(sbyte), ExpressionTypeCode.SByte },
        { typeof(short), ExpressionTypeCode.Int16 },
        { typeof(int), ExpressionTypeCode.Int32 },
        { typeof(long), ExpressionTypeCode.Int64 },
        { typeof(ushort), ExpressionTypeCode.UInt16 },
        { typeof(uint), ExpressionTypeCode.UInt32 },
        { typeof(ulong), ExpressionTypeCode.UInt64 },
        { typeof(float), ExpressionTypeCode.Single },
        { typeof(double), ExpressionTypeCode.Double },
        { typeof(decimal), ExpressionTypeCode.Decimal },
        { typeof(TimeSpan), ExpressionTypeCode.TimeSpan },
        { typeof(DateTime), ExpressionTypeCode.DateTime },
        { typeof(Guid), ExpressionTypeCode.Guid },
        { typeof(char), ExpressionTypeCode.Char },
        { typeof(DateOnly), ExpressionTypeCode.DateOnly },
        { typeof(TimeOnly), ExpressionTypeCode.TimeOnly },
        { typeof(Half), ExpressionTypeCode.Half },
        { typeof(DateTimeOffset), ExpressionTypeCode.DateTimeOffset },

        // well-know nullable value types
        { typeof(bool?), ExpressionTypeCode.NullableBoolean },
        { typeof(byte?), ExpressionTypeCode.NullableByte },
        { typeof(sbyte?), ExpressionTypeCode.NullableSByte },
        { typeof(short?), ExpressionTypeCode.NullableInt16 },
        { typeof(int?), ExpressionTypeCode.NullableInt32 },
        { typeof(long?), ExpressionTypeCode.NullableInt64 },
        { typeof(ushort?), ExpressionTypeCode.NullableUInt16 },
        { typeof(uint?), ExpressionTypeCode.NullableUInt32 },
        { typeof(ulong?), ExpressionTypeCode.NullableUInt64 },
        { typeof(float?), ExpressionTypeCode.NullableSingle },
        { typeof(double?), ExpressionTypeCode.NullableDouble },
        { typeof(decimal?), ExpressionTypeCode.NullableDecimal },
        { typeof(TimeSpan?), ExpressionTypeCode.NullableTimeSpan },
        { typeof(DateTime?), ExpressionTypeCode.NullableDateTime },
        { typeof(Guid?), ExpressionTypeCode.NullableGuid },
        { typeof(char?), ExpressionTypeCode.NullableChar },
        { typeof(DateOnly?), ExpressionTypeCode.NullableDateOnly },
        { typeof(TimeOnly?), ExpressionTypeCode.NullableTimeOnly },
        { typeof(Half?), ExpressionTypeCode.NullableHalf },
        { typeof(DateTimeOffset?), ExpressionTypeCode.NullableDateTimeOffset },

        // well-know primitive reference types
        { typeof(string), ExpressionTypeCode.String },
    };

    /// <summary>
    /// Maps supported well-known <see cref="ExpressionTypeCode"/> enum members to their corresponding <see cref="Type"/>s.
    /// </summary>
    private static readonly Dictionary<ExpressionTypeCode, Type> ReverseLookup = new()
    {
        // default
        { ExpressionTypeCode.Void, typeof(void) },

        // well-know value types
        { ExpressionTypeCode.Boolean, typeof(bool) },
        { ExpressionTypeCode.Byte, typeof(byte)  },
        { ExpressionTypeCode.SByte, typeof(sbyte)  },
        { ExpressionTypeCode.Int16, typeof(short)  },
        { ExpressionTypeCode.Int32, typeof(int) },
        { ExpressionTypeCode.Int64, typeof(long) },
        { ExpressionTypeCode.UInt16, typeof(ushort) },
        { ExpressionTypeCode.UInt32, typeof(uint) },
        { ExpressionTypeCode.UInt64, typeof(ulong) },
        { ExpressionTypeCode.Single, typeof(float) },
        { ExpressionTypeCode.Double, typeof(double) },
        { ExpressionTypeCode.Decimal, typeof(decimal) },
        { ExpressionTypeCode.TimeSpan, typeof(TimeSpan) },
        { ExpressionTypeCode.DateTime, typeof(DateTime) },
        { ExpressionTypeCode.Guid, typeof(Guid) },
        { ExpressionTypeCode.Char, typeof(char) },
        { ExpressionTypeCode.DateOnly, typeof(DateOnly) },
        { ExpressionTypeCode.TimeOnly, typeof(TimeOnly) },
        { ExpressionTypeCode.Half, typeof(Half) },
        { ExpressionTypeCode.DateTimeOffset, typeof(DateTimeOffset) },

        // well-know nullable value types
        { ExpressionTypeCode.NullableBoolean, typeof(bool?) },
        { ExpressionTypeCode.NullableByte, typeof(byte?) },
        { ExpressionTypeCode.NullableSByte, typeof(sbyte?) },
        { ExpressionTypeCode.NullableInt16, typeof(short?) },
        { ExpressionTypeCode.NullableInt32, typeof(int?) },
        { ExpressionTypeCode.NullableInt64, typeof(long?) },
        { ExpressionTypeCode.NullableUInt16, typeof(ushort?) },
        { ExpressionTypeCode.NullableUInt32, typeof(uint?) },
        { ExpressionTypeCode.NullableUInt64, typeof(ulong?) },
        { ExpressionTypeCode.NullableSingle, typeof(float?) },
        { ExpressionTypeCode.NullableDouble, typeof(double?) },
        { ExpressionTypeCode.NullableDecimal, typeof(decimal?) },
        { ExpressionTypeCode.NullableTimeSpan, typeof(TimeSpan?) },
        { ExpressionTypeCode.NullableDateTime, typeof(DateTime?) },
        { ExpressionTypeCode.NullableGuid, typeof(Guid?) },
        { ExpressionTypeCode.NullableChar, typeof(char?) },
        { ExpressionTypeCode.NullableDateOnly, typeof(DateOnly?) },
        { ExpressionTypeCode.NullableTimeOnly, typeof(TimeOnly?) },
        { ExpressionTypeCode.NullableHalf, typeof(Half?) },
        { ExpressionTypeCode.NullableDateTimeOffset, typeof(DateTimeOffset?) },

        // well-know primitive reference types
        { ExpressionTypeCode.String, typeof(string) },
    };

    private static readonly HashSet<ExpressionTypeCode> NumericLookup = new()
    {
        // well-know value types
        { ExpressionTypeCode.Int16 },
        { ExpressionTypeCode.Int32 },
        { ExpressionTypeCode.Int64 },
        { ExpressionTypeCode.UInt16 },
        { ExpressionTypeCode.UInt32 },
        { ExpressionTypeCode.UInt64 },
        { ExpressionTypeCode.Single },
        { ExpressionTypeCode.Double },
        { ExpressionTypeCode.Decimal },
        { ExpressionTypeCode.Half },

        // well-know nullable value types
        { ExpressionTypeCode.NullableInt16 },
        { ExpressionTypeCode.NullableInt32 },
        { ExpressionTypeCode.NullableInt64 },
        { ExpressionTypeCode.NullableUInt16 },
        { ExpressionTypeCode.NullableUInt32 },
        { ExpressionTypeCode.NullableUInt64 },
        { ExpressionTypeCode.NullableSingle },
        { ExpressionTypeCode.NullableDouble },
        { ExpressionTypeCode.NullableDecimal },
        { ExpressionTypeCode.NullableHalf },
    };

    private static readonly HashSet<ExpressionTypeCode> DateTimeTypes = new()
    {
        // well-know value types
        { ExpressionTypeCode.DateTime },
        { ExpressionTypeCode.DateOnly },
        { ExpressionTypeCode.TimeOnly },
        { ExpressionTypeCode.DateTimeOffset },

        // well-know nullable value types
        { ExpressionTypeCode.NullableDateTime },
        { ExpressionTypeCode.NullableDateOnly },
        { ExpressionTypeCode.NullableTimeOnly },
        { ExpressionTypeCode.NullableDateTimeOffset },
    };

    /// <summary>
    /// Attempts to get the <see cref="ExpressionTypeCode"/> for the specified <see cref="Type"/>.
    /// </summary>
    public static bool TryGetExpressionTypeCode(this Type type, out ExpressionTypeCode expressionTypeCode)
    {
        Guard.IsNotNull(type);

        return ForwardLookup.TryGetValue(type, out expressionTypeCode);
    }

    /// <summary>
    /// Gets the <see cref="ExpressionTypeCode"/> for the specified <see cref="Type"/>.
    /// Throws <see cref="ArgumentException"/> if the <see cref="Type"/> is not supported.
    /// </summary>
    public static ExpressionTypeCode GetExpressionTypeCode(this Type type)
    {
        Guard.IsNotNull(type);

        if (TryGetExpressionTypeCode(type, out var expressionTypeCode))
        {
            return expressionTypeCode;
        }

        return ThrowHelper.ThrowArgumentException<ExpressionTypeCode>(nameof(type), $"{nameof(Type)} '{type.FullName}' does not have a corresponding {nameof(ExpressionTypeCode)}");
    }

    /// <summary>
    /// Attempts to get the <see cref="Type"/> for the specified <see cref="ExpressionTypeCode"/>.
    /// </summary>
    public static bool TryGetSystemType(this ExpressionTypeCode expressionTypeCode, [MaybeNullWhen(false)] out Type systemType)
    {
        return ReverseLookup.TryGetValue(expressionTypeCode, out systemType);
    }

    /// <summary>
    /// Gets the <see cref="Type"/> for the specified <see cref="ExpressionTypeCode"/>.
    /// Throws <see cref="ArgumentException"/> if the <see cref="ExpressionTypeCode"/> is not supported.
    /// </summary>
    public static Type GetSystemType(this ExpressionTypeCode expressionTypeCode)
    {
        if (TryGetSystemType(expressionTypeCode, out var systemType))
        {
            return systemType;
        }

        return ThrowHelper.ThrowArgumentException<Type>(nameof(expressionTypeCode), $"{nameof(ExpressionTypeCode)} '{expressionTypeCode}' does not have a corresponding {nameof(Type)}");
    }

    /// <summary>
    /// Determines if the specified <see cref="ExpressionTypeCode"/> represents a <see cref="ValueType"/>.
    /// </summary>
    public static bool IsValueType(this ExpressionTypeCode expressionTypeCode)
    {
        if (!ReverseLookup.TryGetValue(expressionTypeCode, out var type))
        {
            // this should never happen
            return ThrowHelper.ThrowArgumentOutOfRangeException<bool>(nameof(expressionTypeCode));
        }

        return type.IsValueType;
    }

    /// <summary>
    /// Determines if the specified <see cref="ExpressionTypeCode"/> represents a nullable <see cref="ValueType"/>.
    /// </summary>
    public static bool IsNullableValueType(this ExpressionTypeCode expressionTypeCode)
    {
        if (!ReverseLookup.TryGetValue(expressionTypeCode, out var type))
        {
            // this should never happen
            return ThrowHelper.ThrowArgumentOutOfRangeException<bool>(nameof(expressionTypeCode));
        }

        return Nullable.GetUnderlyingType(type) is not null;
    }

    /// <summary>
    /// Determines if the specified <see cref="ExpressionTypeCode"/> represents a reference <see cref="Type"/>.
    /// This is the opposite of calling <see cref="IsValueType(ExpressionTypeCode)"/>.
    /// </summary>
    public static bool IsReferenceType(this ExpressionTypeCode expressionTypeCode) => !IsValueType(expressionTypeCode);

    /// <summary>
    /// Determines if the specified <see cref="ExpressionTypeCode"/> represents one of the supported numeric types.
    /// This excludes the byte types.
    /// </summary>
    public static bool IsNumericType(this ExpressionTypeCode expressionTypeCode) => NumericLookup.Contains(expressionTypeCode);

    /// <summary>
    /// Determines if the specified <see cref="ExpressionTypeCode"/> represents one of the supported date or time types.
    /// </summary>
    public static bool IsDateOrTimeType(this ExpressionTypeCode expressionTypeCode) => DateTimeTypes.Contains(expressionTypeCode);
}