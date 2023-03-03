namespace Unosquare.DateTimeExt.Test;

public class YearMonthTests
{
    [Fact]
    public void WithCurrentMonth_DistinctDates()
    {
        var yearMonth = new YearMonth(DateTime.UtcNow);
        var monthDate = new MonthToDate(DateTime.UtcNow);
        var lastDayOfMonth = DateTime.UtcNow.GetLastBusinessDayOfMonth().Date;

        Assert.Equal(lastDayOfMonth, yearMonth.EndDate);
        Assert.Equal(DateTime.UtcNow.Date, monthDate.EndDate);
    }

    [Fact]
    public void WithMonth_OutOfRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new YearMonth(month: 13, year: 2022));
    }

    [Fact]
    public void WithMonth_Deconstruct()
    {
        var yearMonth = new YearMonth(month: 1);
        yearMonth.Deconstruct(out DateTime startDate, out var endDate);

        Assert.Equal(new(DateTime.UtcNow.Year, 1, 1), startDate);
        Assert.Equal(new(DateTime.UtcNow.Year, 1, 31), endDate);
    }

    [Fact]
    public void WithMonth_Next()
    {
        var yearMonth = new YearMonth(month: 1, year: 2022);

        Assert.Equal(new(2022, 2, 1), yearMonth.Next.StartDate);
    }

    [Fact]
    public void WithMonth_Previous()
    {
        var yearMonth = new YearMonth(month: 2, year: 2022);

        Assert.Equal(new(2022, 1, 1), yearMonth.Previous.StartDate);
    }

    [Fact]
    public void WithMonth_ToMonth()
    {
        var yearMonth = new YearMonth(month: 1, year: 2022);
        var endYear = yearMonth.ToMonth(12);

        Assert.Equal(new(2022, 12, 1), endYear.StartDate);
    }

    [Fact]
    public void WithMonth_AddMonths()
    {
        var yearMonth = new YearMonth(month: 1, year: 2022);
        var endYear = yearMonth.AddMonths(11);

        Assert.Equal(new(2022, 12, 1), endYear.StartDate);
    }

    [Fact]
    public void WithYearMonth_ReturnsFormattedString()
    {
        Assert.Equal("2022-01", new YearMonth(year: 2022, month: 1).ToString());
    }

    [Fact]
    public void WithYearMonth_TryParse()
    {
        var result = YearMonth.TryParse("2022-01", out var dateRange);
        Assert.True(result);
        Assert.Equal(new(year: 2022, month: 1), dateRange);
    }

    [Fact]
    public void WithTwoQuarters_ComparesThem()
    {
        var yearQuarterOne = new YearMonth(year: 2022, month: 1);
        var yearQuarterTwo = new YearMonth(year: 2022, month: 2);

        Assert.Equal(-1, yearQuarterOne.CompareTo(yearQuarterTwo));
        Assert.Equal(0, yearQuarterOne.CompareTo(yearQuarterOne));
        Assert.Equal(1, yearQuarterTwo.CompareTo(yearQuarterOne));
    }

    [Fact]
    public void WithYearMonthDay_ReturnsDateTime()
    {
        Assert.Equal(new(2022, 1, 10), new YearMonth(year: 2022, month: 1).Day(10));
    }

    [Fact]
    public void WithYearMontCurrent_IsCurrentTrue()
    {
        Assert.True(YearMonth.Current.IsCurrent);
    }
}