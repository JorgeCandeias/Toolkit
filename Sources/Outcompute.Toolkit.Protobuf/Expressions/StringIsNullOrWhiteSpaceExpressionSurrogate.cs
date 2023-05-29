namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class StringIsNullOrWhiteSpaceExpressionSurrogate : QueryExpressionSurrogate
{
    [ProtoMember(1)]
    public QueryExpressionSurrogate Target { get; set; } = null!;

    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitStringIsNullOrWhiteSpace(this);
}