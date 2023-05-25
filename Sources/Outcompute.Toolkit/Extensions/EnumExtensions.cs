namespace Outcompute.Toolkit.Extensions;

/// <summary>
/// Quality-of-life and efficiency extensions for <see cref="Enum"/>.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Type rooting pattern for fast type-keyed lookups.
    /// </summary>
    private static class TypeRoot<T> where T : struct, Enum
    {
        static TypeRoot()
        {
            var values = Enum.GetValues<T>();
            var names = Enum.GetNames<T>();

            for (var i = 0; i < values.Length; i++)
            {
                Names.Add(values[i], names[i]);
            }
        }

        /// <summary>
        /// Caches enum member names.
        /// </summary>
        public static readonly Dictionary<T, string> Names = new();
    }

    /// <summary>
    /// Sames as <see cref="Enum.ToString"/> or <see cref="Enum.GetName{TEnum}(TEnum)"/> but without allocating after the first call.
    /// </summary>
    public static string AsString<T>(this T value) where T : struct, Enum => TypeRoot<T>.Names[value];
}