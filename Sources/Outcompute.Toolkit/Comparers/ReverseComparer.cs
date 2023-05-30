namespace Outcompute.Toolkit.Comparers;

/// <summary>
/// A comparer that reverses the result of another comparer.
/// </summary>
public class ReverseComparer<T> : Comparer<T>
{
    private readonly IComparer<T> _comparer;

    internal ReverseComparer(IComparer<T>? comparer = null)
    {
        _comparer = comparer ?? Comparer<T>.Default;
    }

    public override int Compare(T? x, T? y) => -_comparer.Compare(x, y);

    /// <summary>
    /// Gets the default <see cref="ReverseComparer{T}"/> that does not delegate to another comparer.
    /// </summary>
    public new static ReverseComparer<T> Default { get; } = new ReverseComparer<T>();
}

/// <summary>
/// Organizes the various ways to create a <see cref="ReverseComparer{T}"/>.
/// </summary>
public static class ReverseComparer
{
    public static ReverseComparer<T> Create<T>(IComparer<T>? comparer = null)
    {
        return new(comparer);
    }
}