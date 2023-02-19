namespace Outcompute.Toolkit.Core.CodeGenerator;

[ExcludeFromCodeCoverage]
internal sealed class CodeWriter : IDisposable
{
    private readonly ArrayPoolBufferWriter<char> _writer = new();

    private int _ident = 0;

    private CodeWriter Ident()
    {
        for (var i = 0; i < _ident; i++)
        {
            _writer.Write('\t');
        }

        return this;
    }

    private CodeWriter Nest()
    {
        _ident++;

        return this;
    }

    private CodeWriter Unnest()
    {
        _ident--;

        return this;
    }

    public CodeWriter Write(string value)
    {
        _writer.Write(value);

        return this;
    }

    public CodeWriter Line() => Write("\r\n");

    public CodeWriter Line(string value) => Ident().Write(value).Line();

    public CodeWriter Open(string value) => Line(value).Line("{").Nest();

    public CodeWriter Close() => Unnest().Line("}");

    public CodeWriter CloseColon() => Unnest().Line("};");

    public override string ToString() => _writer.ToString();

    public void Dispose()
    {
        _writer.Dispose();
    }
}