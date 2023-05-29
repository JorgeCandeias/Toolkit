namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class ConstantExpressionSurrogate<T> : QueryExpressionSurrogate
{
    [ProtoMember(1)]
    public T Value { get; set; } = default!;

    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitConstant(this);
}