namespace Outcompute.Toolkit.Core.CodeGenerator;

[Generator]
[ExcludeFromCodeCoverage]
internal class EnumSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context
            .SyntaxProvider.CreateSyntaxProvider(Predicate, Transform)
            .Where(x => x is not null)
            .Collect();

        context.RegisterSourceOutput(provider, GenerateCode);
    }

    private bool Predicate(SyntaxNode syntaxNode, CancellationToken cancellationToken)
    {
        if (syntaxNode is not IdentifierNameSyntax)
        {
            return false;
        }

        return true;
    }

    private ITypeSymbol Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        if (context.SemanticModel.GetSymbolInfo(context.Node, cancellationToken).Symbol is not ITypeSymbol symbol)
        {
            return null;
        }

        if (symbol.TypeKind is not TypeKind.Enum)
        {
            return null;
        }

        return symbol;
    }

    private void GenerateCode(SourceProductionContext context, ImmutableArray<ITypeSymbol> candidates)
    {
        var symbols = candidates.Distinct<ITypeSymbol>(SymbolEqualityComparer.Default);

        foreach (var symbol in symbols)
        {
            var ns = symbol.ContainingNamespace.IsGlobalNamespace ? null : $"{symbol.ContainingNamespace.Name}.";
            var name = symbol.ToDisplayString();
            var title = $"Outcompute.Toolkit.Core.Extensions.{ns}{name}.g.cs";

            //context.AddSource(title, GenerateCode(symbol, ns));
        }
    }

    private static string GenerateCode(ITypeSymbol symbol, string ns)
    {
        using var writer = new CodeWriter();

        writer
            .Line($"namespace {ns};")
            .Line()
            .Open("internal static partial class EnumExtensions")
            .Open($"internal static string AsString(this {symbol.ToDisplayString()} value)")
            .Open("return value switch");

        foreach (var member in symbol.GetMembers())
        {
            if (member.Kind == SymbolKind.Field)
            {
                writer.Line($@"{member.ToDisplayString()} => ""{member.Name}"",");
            }
        }

        writer
            .CloseColon()
            .Close()
            .Close();

        return writer.ToString();
    }
}