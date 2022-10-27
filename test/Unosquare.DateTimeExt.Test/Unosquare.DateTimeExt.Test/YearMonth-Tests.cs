namespace Unosquare.DateTimeExt.Test;

public class YearMonthTests
{
    [Fact]
    public void WithQuarter_Deconstruct()
    {
        var yearMonth = new YearMonth(month: 1);
        yearMonth.Deconstruct(out DateTime startDate, out var endDate);

        Assert.Equal(new(2022, 1, 1), startDate);
        Assert.Equal(new(2022, 1, 31), endDate);
    }

    [Fact]
    public void WithYearQuarter_ReturnsFormattedString()
    {
        Assert.Equal("2022-1", new YearMonth(year: 2022, month: 1).ToString());
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
}