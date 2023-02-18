<<<<<<< HEAD
﻿using System.Collections.Generic;

namespace Outcompute.Toolkit.Core.CodeGenerator;
=======
﻿namespace Outcompute.Toolkit.Core.CodeGenerator;
>>>>>>> 255046ca48c894877bc9972a47ad634cbdcf9df2

[Generator]
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

    // this code runs on any code change and must be as fast as possible
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

<<<<<<< HEAD
        context.AddSource("Outcompute.Toolkit.Core.Extensions.EnumExtensions.g.cs", GenerateEntryCode(symbols));

        /*
=======
>>>>>>> 255046ca48c894877bc9972a47ad634cbdcf9df2
        foreach (var symbol in symbols)
        {
            var prefix1 = "Outcompute.Toolkit.";
            var prefix2 = symbol.ContainingNamespace.IsGlobalNamespace ? null : $"{symbol.ContainingNamespace}.";
            var title = $"{prefix1}{prefix2}{symbol.Name}.g.cs";

            var code = GenerateCode(symbol);

            context.AddSource(title, code);
        }
<<<<<<< HEAD
        */
    }

    private string GenerateEntryCode(IEnumerable<ITypeSymbol> symbols)
    {
        using var writer = new CodeWriter();

        return writer
            .Line("namespace Outcompute.Toolkit.Core.Extensions;")
            .Line()
            .OpenBlock("public static partial class EnumExtensions")
            .OpenBlock("public static partial string AsString<T>(this T value) where T : struct, Enum")
            .Line("return null;")
            .CloseBlock()
            .CloseBlock()
            .ToString();
=======
>>>>>>> 255046ca48c894877bc9972a47ad634cbdcf9df2
    }

    private string GenerateCode(ITypeSymbol symbol)
    {
        var ns = GetNameSpace(symbol);

        return $@"

<<<<<<< HEAD
namespace Outcompute.Toolkit.Core.Extensions;

public static partial class EnumExtensions
{{
    public static partial string AsString<T>(this T value) where T : struct, Enum;
=======
{(ns is null ? null : ($"namespace {ns};"))}

public static class {symbol.Name}OutcomputeToolkitExtensions
{{
    public static string AsString(this {symbol.Name} value)
>>>>>>> 255046ca48c894877bc9972a47ad634cbdcf9df2
    {{
        return value switch
        {{
            _ => ""ABC""
        }};
    }}
}}

";
    }

    private string GetNameSpace(ISymbol symbol) => symbol.ContainingNamespace.IsGlobalNamespace ? null : symbol.ContainingNamespace.ToString();
}