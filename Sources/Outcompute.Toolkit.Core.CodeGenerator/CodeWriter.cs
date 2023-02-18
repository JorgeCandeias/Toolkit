namespace Outcompute.Toolkit.Core.CodeGenerator;

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

    private CodeWriter Write(string value)
    {
        _writer.Write(value);

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

    public CodeWriter Line()
    {
        _writer.Write("\r\n");

        return this;
    }

    public CodeWriter Line(string value) => Write(value).Line();

    public CodeWriter OpenBlock() => Ident().Line("{").Nest();

    public CodeWriter OpenBlock(string value) => Ident().Write(value).Line().Nest();

    public CodeWriter CloseBlock() => Ident().Line("}").Unnest();

    public override string ToString() => _writer.ToString();

    public void Dispose()
    {
        _writer.Dispose();
    }
}