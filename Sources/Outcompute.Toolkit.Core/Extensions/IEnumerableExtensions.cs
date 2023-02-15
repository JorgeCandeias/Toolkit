namespace Outcompute.Toolkit.Core.Extensions;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Copies the items from the specified <paramref name="source"/> into a new <see cref="DataTable"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="DataTable"/> class is inneficient and puts significant pressure on the garbage collector.
    /// Use this extension only for integration with legacy code where refactoring is not yet an option.
    /// </remarks>
    public static DataTable ToDataTable<T>(this IEnumerable<T> source)
    {
        Guard.IsNotNull(source);

        // pin locals for max perf
        var table = new DataTable();
        var properties = TypeDescriptor.GetProperties(typeof(T));
        var count = properties.Count;
        var columns = table.Columns;
        var rows = table.Rows;

        // populate columns
        for (var i = 0; i < count; i++)
        {
            var property = properties[i];
            var type = property.PropertyType;

            if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type)!;
            }

            columns.Add(property.Name, type);
        }

        // populate rows
        var values = new object?[count];

        foreach (var item in source)
        {
            for (var i = 0; i < count; i++)
            {
                values[i] = properties[i].GetValue(item);
            }

            rows.Add(values);
        }

        return table;
    }
}