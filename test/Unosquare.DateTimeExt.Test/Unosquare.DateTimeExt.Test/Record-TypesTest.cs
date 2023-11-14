namespace Unosquare.DateTimeExt.Test;

public class RecordTypesTests
{
    [Fact]
    public void DateRangeRecord_ShouldCreateInstance_WithValidDates()
    {
        // Arrange
        var startDate = new DateTime(2022, 1, 1);
        var endDate = new DateTime(2022, 1, 31);

        // Act
        var dateRange = new DateRangeRecord { StartDate = startDate, EndDate = endDate};

        // Assert
        Assert.Equal(startDate, dateRange.StartDate);
        Assert.Equal(endDate, dateRange.EndDate);
    }

    [Fact]
    public void YearMonthRecord_ShouldCreateInstance_WithValidYearAndMonth()
    {
        // Arrange
        const int year = 2022;
        const int month = 1;

        // Act
        var yearMonth = new YearMonthRecord { Year = year, Month = month };

        // Assert
        Assert.Equal(year, yearMonth.Year);
        Assert.Equal(month, yearMonth.Month);
    }

    [Fact]
    public void YearQuarterRecord_ShouldCreateInstance_WithValidYearAndQuarter()
    {
        // Arrange
        const int year = 2022;
        const int quarter = 1;

        // Act
        var yearQuarter = new YearQuarterRecord { Year = year, Quarter = quarter };

        // Assert
        Assert.Equal(year, yearQuarter.Year);
        Assert.Equal(quarter, yearQuarter.Quarter);
    }

    [Fact]
    public void YearWeekRecord_ShouldCreateInstance_WithValidYearAndWeek()
    {
        // Arrange
        const int year = 2022;
        const int week = 1;

        // Act
        var yearWeek = new YearWeekRecord { Year = year, Week = week };

        // Assert
        Assert.Equal(year, yearWeek.Year);
        Assert.Equal(week, yearWeek.Week);
    }
}