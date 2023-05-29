using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Protobuf.Expressions;

public static class QueryExpressionProtobufServiceCollectionExtensions
{
    /// <summary>
    /// Enables protobuf support for <see cref="QueryExpression"/> types.
    /// </summary>
    public static IServiceCollection AddProtobufQueryExpressions(this IServiceCollection services)
    {
        Guard.IsNotNull(services);

        RuntimeTypeModel.Default.Add(typeof(QueryExpression), false).SetSurrogate(typeof(QueryExpressionSurrogate));

        return services;
    }
}
