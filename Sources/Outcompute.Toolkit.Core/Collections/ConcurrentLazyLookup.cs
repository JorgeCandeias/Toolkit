namespace Outcompute.Toolkit.Core.Collections;

/// <summary>
/// A lookup where the value is created upon first access from a pre-configured factory.
/// </summary>
public class ConcurrentLazyLookup<TKey, TValue> : IReadOnlyDictionary<TKey, TValue> where TKey : notnull
{
    public ConcurrentLazyLookup(Func<TKey, TValue> factory)
    {
        Guard.IsNotNull(factory);

        _factory = factory;
    }

    /// <summary>
    /// Holds the items in this lookup.
    /// </summary>
    private readonly ConcurrentDictionary<TKey, TValue> _dictionary = new();

    /// <summary>
    /// Holds the factory used to create the items.
    /// </summary>
    private readonly Func<TKey, TValue> _factory;

    /// <summary>
    /// Gets a collection containing the keys in the lookup.
    /// </summary>
    public IEnumerable<TKey> Keys => _dictionary.Keys;

    /// <summary>
    /// Gets a collection containing the values in the lookup.
    /// </summary>
    public IEnumerable<TValue> Values => _dictionary.Values;

    /// <summary>
    /// Gets the number of key/value pairs in the lookup.
    /// </summary>
    public int Count => _dictionary.Count;

    /// <summary>
    /// Gets the value associated with the specified key.
    /// If the value does not yet exist then it is created and added to the lookup and then returned.
    /// </summary>
    public TValue this[TKey key] => _dictionary.GetOrAdd(key, _factory);

    /// <summary>
    /// Determines whether the lookup contains the specified key.
    /// This method does not create the associated value for the key if it does not exist yet.
    /// </summary>
    /// <returns>
    /// <see cref="true"/> if the <see cref="ConcurrentLazyLookup{TKey, TValue}"/> contains the specified <paramref name="key"/>, otherwise <see cref="false"/>.
    /// </returns>
    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

    /// <summary>
    /// Gets the value associated with the specified key.
    /// If the value does not yet exist then it is created and added to the lookup and then returned.
    /// </summary>
    /// <returns>
    /// Always returns <see cref="true"/>.
    /// </returns>
    public bool TryGetValue(TKey key, out TValue value)
    {
        value = _dictionary.GetOrAdd(key, _factory);
        return true;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="ConcurrentLazyLookup{TKey, TValue}"/>.
    /// </summary>
    /// <returns>
    /// An enumerator for the <see cref="ConcurrentLazyLookup{TKey, TValue}"/>.
    /// </returns>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

    /// <inheritdoc cref="GetEnumerator"/>
    IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
}