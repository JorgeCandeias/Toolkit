namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class DefaultExpressionSurrogate<TValue> : QueryExpressionSurrogate
{
    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitDefault<TValue>(this);
}
