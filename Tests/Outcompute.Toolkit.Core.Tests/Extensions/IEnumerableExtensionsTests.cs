using System.Data;

namespace Outcompute.Toolkit.Core.Tests.Extensions;

public class IEnumerableExtensionsTests
{
    [Fact]
    public void ConvertsIEnumerableToDataTable()
    {
        // arrange
        var source = new[]
        {
            new { A = 1, B = "2", C = 3M },
            new { A = 4, B = "5", C = 6M },
            new { A = 7, B = "8", C = 9M },
        };

        // act
        using var result = source.ToDataTable();

        // assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Columns.Count);
        Assert.Equal(3, result.Rows.Count);
        Assert.Equal(typeof(int), result.Columns["A"]!.DataType);
        Assert.Equal(typeof(string), result.Columns["B"]!.DataType);
        Assert.Equal(typeof(decimal), result.Columns["C"]!.DataType);
        Assert.Equal(source, result.AsEnumerable().Select(x => new { A = (int)x["A"], B = (string)x["B"], C = (decimal)x["C"] }));
    }
}