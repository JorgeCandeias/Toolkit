namespace Outcompute.Toolkit.Core.Extensions;

public static class EnumExtensions
{
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

        public static readonly Dictionary<T, string> Names = new();
    }

    public static string AsString<T>(this T value) where T : struct, Enum => TypeRoot<T>.Names[value];
}