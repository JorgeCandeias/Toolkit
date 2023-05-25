namespace Outcompute.Toolkit.Extensions;

public static class StructExtensions
{
    /// <summary>
    /// Gets a nullable copy of the specified <paramref name="value"/>.
    /// </summary>
    public static T? AsNullable<T>(this T value)
        where T : struct
    {
        return (T?)value;
    }
}