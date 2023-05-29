namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class StringCompareExpressionSurrogate : QueryExpressionSurrogate
{
    [ProtoMember(1)]
    public QueryExpressionSurrogate Target { get; set; } = null!;

    [ProtoMember(2)]
    public QueryExpressionSurrogate Value { get; set; } = null!;

    [ProtoMember(3)]
    public StringComparison Comparison { get; set; }

    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitStringCompare(this);
}