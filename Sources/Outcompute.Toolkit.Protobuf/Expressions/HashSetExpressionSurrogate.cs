using System.Collections.Immutable;

namespace Outcompute.Toolkit.Protobuf.Expressions;

[ProtoContract]
internal record class HashSetExpressionSurrogate<T> : QueryExpressionSurrogate
{
    [ProtoMember(1, OverwriteList = true)]
    public ImmutableHashSet<T> Values { get; set; } = default!;

    protected internal override QueryExpressionSurrogate Accept(ProtobufQueryExpressionSurrogateVisitor visitor) => visitor.VisitHashSet(this);
}