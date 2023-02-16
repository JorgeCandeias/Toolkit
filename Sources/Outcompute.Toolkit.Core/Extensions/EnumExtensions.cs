namespace Outcompute.Toolkit.Core.Extensions;

// todo: replace the implementation below with code generation

/// <summary>
/// Quality-of-life extensions for <see cref="Enum"/>.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Type rooting pattern for fast type-keyed cache.
    /// </summary>
    private static class TypeCache<T> where T : struct, Enum
    {
        static TypeCache()
        {
            var values = Enum.GetValues<T>();
            var names = Enum.GetNames<T>();
            var length = values.Length;

            for (var i = 0; i < length; i++)
            {
                Names.Add(values[i], names[i]);
            }
        }

        /// <summary>
        /// Caches the string representation of each possible value of <see cref="T"/>.
        /// </summary>
        public static readonly Dictionary<T, string> Names = new();
    }

    /// <summary>
    /// Sames as <see cref="Enum.ToString"/> but without allocating after the very first call.
    /// </summary>
    public static string AsString<T>(this T value) where T : struct, Enum
    {
        return TypeCache<T>.Names[value];
    }

    /// <summary>
    /// Sames as <see cref="Enum.ToString"/> for nullable values but without allocating after the very first call.
    /// </summary>
    public static string? AsString<T>(this T? value, string? defaultValue = null) where T : struct, Enum
    {
        if (value.HasValue)
        {
            return TypeCache<T>.Names[value.Value];
        }

        return defaultValue;
    }
}