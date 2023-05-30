using Outcompute.Toolkit.Collections;

namespace Outcompute.Toolkit.Comparers;

/// <summary>
/// Organizes alternative ways to create a default <see cref="Comparer{T}"/>.
/// </summary>
public static class DefaultComparer
{
    /// <summary>
    /// Gets the default comparer for <typeparamref name="T"/>.
    /// </summary>
    public static Comparer<T> Create<T>() => Comparer<T>.Default;

    /// <summary>
    /// Caches delegates for <see cref="Create{T}()"/>.
    /// </summary>
    private static readonly ConcurrentLazyLookup<Type, Func<IComparer>> CreateDelegates = new(type =>
    {
        return ((Func<Comparer<object>>)Create<object>)
            .Method
            .GetGenericMethodDefinition()
            .MakeGenericMethod(type)
            .CreateDelegate<Func<IComparer>>();
    });

    /// <summary>
    /// Gets the default comparer for the specified non-generic <see cref="Type"/>.
    /// The returned comparer is generic and can be cast to <see cref="Comparer{T}"/>,
    /// </summary>
    public static IComparer Create(Type type)
    {
        Guard.IsNotNull(type);

        return CreateDelegates[type]();
    }
}