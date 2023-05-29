namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class AssignExpressionSurrogate : QueryExpressionSurrogate
{
    [ProtoMember(1)]
    public QueryExpressionSurrogate Target { get; set; } = null!;

    [ProtoMember(2)]
    public QueryExpressionSurrogate Value { get; set; } = null!;

    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitAssign(this);
}
