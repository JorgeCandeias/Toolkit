﻿namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class NotEqualExpressionSurrogate : QueryExpressionSurrogate
{
    [ProtoMember(1)]
    public QueryExpressionSurrogate Left { get; set; } = null!;

    [ProtoMember(2)]
    public QueryExpressionSurrogate Right { get; set; } = null!;

    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitNotEqual(this);
}