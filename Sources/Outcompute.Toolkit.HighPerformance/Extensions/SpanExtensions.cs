using System.Text;

namespace Outcompute.Toolkit.HighPerformance.Extensions;

public static class SpanExtensions
{
    /// Uses the shared <see cref="StringPool"/> to get or created a <see cref="string"/> from the specified <see cref="ReadOnlySpan{char}"/>.
    public static string ToPooledString(this ReadOnlySpan<char> span)
    {
        return StringPool.Shared.GetOrAdd(span);
    }

    /// Uses the shared <see cref="StringPool"/> to get or created a <see cref="string"/> from the specified <see cref="Span{char}"/>.
    public static string ToPooledString(this Span<char> span)
    {
        return StringPool.Shared.GetOrAdd(span);
    }

    /// Uses the shared <see cref="StringPool"/> to get or created a <see cref="string"/> from the specified <see cref="ReadOnlySpan{byte}"/>.
    public static string ToPooledString(this ReadOnlySpan<byte> span, Encoding encoding)
    {
        return StringPool.Shared.GetOrAdd(span, encoding);
    }

    /// Uses the shared <see cref="StringPool"/> to get or created a <see cref="string"/> from the specified <see cref="Span{byte}"/>.
    public static string ToPooledString(this Span<byte> span, Encoding encoding)
    {
        return StringPool.Shared.GetOrAdd(span, encoding);
    }
}