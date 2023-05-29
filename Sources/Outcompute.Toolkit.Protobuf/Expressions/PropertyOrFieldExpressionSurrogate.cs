﻿namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class PropertyOrFieldExpressionSurrogate : QueryExpressionSurrogate
{
    [ProtoMember(1)]
    public QueryExpressionSurrogate Target { get; set; } = null!;

    [ProtoMember(2)]
    public string Name { get; set; } = null!;

    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitPropertyOrField(this);
}
