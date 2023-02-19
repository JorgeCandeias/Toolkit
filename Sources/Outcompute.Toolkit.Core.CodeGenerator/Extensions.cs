namespace Outcompute.Toolkit.Core.CodeGenerator;

[ExcludeFromCodeCoverage]
internal static class Extensions
{
    public static void Write(this IBufferWriter<char> writer, string value)
    {
        Guard.IsNotNull(writer);
        Guard.IsNotNull(value);

        var buffer = writer.GetSpan(value.Length);
        value.AsSpan().CopyTo(buffer);
        writer.Advance(value.Length);
    }

    public static void WriteIdented(this IBufferWriter<char> writer, string value, int ident)
    {
        Guard.IsNotNull(writer);
        Guard.IsNotNull(value);
        Guard.IsGreaterThanOrEqualTo(ident, 0);

        writer.Ident(ident);
        writer.Write(value);
    }

    public static void WriteLine(this IBufferWriter<char> writer, string value)
    {
        Guard.IsNotNull(writer);
        Guard.IsNotNull(value);

        writer.Write(value);
        writer.Write("\r\n");
    }

    public static void WriteIdentedLine(this IBufferWriter<char> writer, string value, int ident)
    {
        Guard.IsNotNull(writer);
        Guard.IsNotNull(value);
        Guard.IsGreaterThanOrEqualTo(ident, 0);

        writer.Ident(ident);
        writer.WriteLine(value);
    }

    public static void Ident(this IBufferWriter<char> writer, int ident)
    {
        Guard.IsNotNull(writer);
        Guard.IsGreaterThanOrEqualTo(ident, 0);

        for (var i = 0; i < ident; i++)
        {
            writer.Write('\t');
        }
    }
}