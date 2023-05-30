using Outcompute.Toolkit.Expressions;

namespace Outcompute.Toolkit.Protobuf.Expressions;

public static class QueryExpressionProtobufServiceCollectionExtensions
{
    /// <summary>
    /// Enables protobuf support for <see cref="WireExpression"/> types.
    /// </summary>
    public static IServiceCollection AddProtobufQueryExpressions(this IServiceCollection services)
    {
        Guard.IsNotNull(services);

        RuntimeTypeModel.Default.Add(typeof(WireExpression), false).SetSurrogate(typeof(QueryExpressionSurrogate));

        return services;
    }
}
