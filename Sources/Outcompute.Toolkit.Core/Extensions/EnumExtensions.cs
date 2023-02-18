namespace Outcompute.Toolkit.Core.Extensions;

/// <summary>
/// Quality-of-life extensions for <see cref="Enum"/>.
/// </summary>
public static partial class EnumExtensions
{
    /// <summary>
    /// Sames as <see cref="Enum.ToString"/> but without allocating after the very first call.
    /// </summary>
    public static partial string AsString<T>(this T value) where T : struct, Enum;

    /// <summary>
    /// Sames as <see cref="Enum.ToString"/> for nullable values but without allocating after the very first call.
    /// </summary>
    public static partial string? AsString<T>(this T? value, string? defaultValue = null) where T : struct, Enum;
}