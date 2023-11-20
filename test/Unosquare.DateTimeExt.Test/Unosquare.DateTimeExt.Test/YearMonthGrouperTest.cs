namespace Unosquare.DateTimeExt.Test;

public class YearMonthGrouperTests
{
    private readonly List<YearMonthRecordWithData> _data = new()
    {
        new() { Year = 2022, Month = 1, Total = 5 },
        new() { Year = 2022, Month = 2, Total = 6 },
        new() { Year = 2022, Month = 2, Total = 1 },
        new() { Year = 2022, Month = 1, Total = 1 },
        new() { Year = 2022, Month = 2, Total = 1 },
        new() { Year = 2022, Month = 1, Total = 1 }
    };

    [Fact]
    public void GroupByDateRange_ShouldGroupByYearMonth()
    {
        // Arrange
        var grouper = new YearMonthGrouper<YearMonthRecordWithData>(_data);

        // Act
        var result = grouper.GroupByDateRange().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(new(1, 2022), result[0].Key);
        Assert.Equal(new(2, 2022), result[1].Key);
        Assert.Equal(3, result[0].Count());
        Assert.Equal(3, result[1].Count());
    }

    [Fact]
    public void GroupByLabel_ShouldGroupByLabel()
    {
        // Arrange
        var grouper = new YearMonthGrouper<YearMonthRecordWithData>(_data);

        // Act
        var result = grouper.GroupByLabel().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("2022-01", result[0].Key);
        Assert.Equal("2022-02", result[1].Key);
        Assert.Equal(3, result[0].Value.Count);
        Assert.Equal(3, result[1].Value.Count);
    }

    [Fact]
    public void GroupByLabel_ShouldGroupByLabelWithTotals()
    {
        // Arrange
        var grouper = new YearMonthGrouper<YearMonthRecordWithData>(_data);

        // Act
        var result = grouper.GroupByLabel(x => x.Total).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("2022-02", result[1].Key);
        Assert.Equal(7, result[0].Value.Sum());
        Assert.Equal(8, result[1].Value.Sum());
    }

    [Fact]
    public void GroupByCount_ShouldGroupByCount()
    {
        // Arrange
        var grouper = new YearMonthGrouper<YearMonthRecordWithData>(_data);

        // Act
        var result = grouper.GroupCount().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("2022-02", result[1].Key);
        Assert.Equal(3, result[0].Value);
        Assert.Equal(3, result[1].Value);
    }

    private sealed record YearMonthRecordWithData : YearMonthRecord
    {
        public int Total { get; init; }
    }
}