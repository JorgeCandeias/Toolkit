namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class ItemExpressionSurrogate : QueryExpressionSurrogate
{
    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitItem(this);
}
