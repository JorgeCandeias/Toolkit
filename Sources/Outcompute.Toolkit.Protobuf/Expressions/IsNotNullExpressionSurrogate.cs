namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class IsNotNullExpressionSurrogate : QueryExpressionSurrogate
{
    [ProtoMember(1)]
    public QueryExpressionSurrogate Target { get; set; } = null!;

    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitIsNotNull(this);
}