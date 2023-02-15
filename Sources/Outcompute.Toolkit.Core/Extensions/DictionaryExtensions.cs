namespace Outcompute.Toolkit.Core.Extensions;

public static class DictionaryExtensions
{
    /// <summary>
    /// Adds the specified <paramref name="items"/> to the <paramref name="dictionary"/>.
    /// </summary>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
        Guard.IsNotNull(dictionary);
        Guard.IsNotNull(items);

        foreach (var item in items)
        {
            dictionary.Add(item.Key, item.Value);
        }
    }

    /// <summary>
    /// Adds the specified <paramref name="items"/> to the <paramref name="dictionary"/>.
    /// </summary>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<(TKey Key, TValue Value)> items)
    {
        Guard.IsNotNull(dictionary);
        Guard.IsNotNull(items);

        foreach (var (key, value) in items)
        {
            dictionary.Add(key, value);
        }
    }

    /// <summary>
    /// Replaces the content in the <paramref name="dictionary"/> with the specified <paramref name="items"/>.
    /// </summary>
    public static void ReplaceWith<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
        Guard.IsNotNull(dictionary);
        Guard.IsNotNull(items);

        dictionary.Clear();
        dictionary.AddRange(items);
    }

    /// <summary>
    /// Replaces the content in the <paramref name="dictionary"/> with the specified <paramref name="items"/>.
    /// </summary>
    public static void ReplaceWith<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<(TKey Key, TValue Value)> items)
    {
        Guard.IsNotNull(dictionary);
        Guard.IsNotNull(items);

        dictionary.Clear();
        dictionary.AddRange(items);
    }

    /// <summary>
    /// Gets the value associated with the specified key, or creates and adds a new value using the default constructor if no value is associated yet.
    /// </summary>
    public static TValue GetOrAddNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        where TValue : new()
    {
        Guard.IsNotNull(dictionary);

        if (!dictionary.TryGetValue(key, out var value))
        {
            dictionary[key] = value = new TValue();
        }

        return value;
    }
}