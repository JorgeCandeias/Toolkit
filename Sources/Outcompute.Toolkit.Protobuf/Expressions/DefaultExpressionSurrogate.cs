namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class DefaultExpressionSurrogate : QueryExpressionSurrogate
{
    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitDefault(this);
}
