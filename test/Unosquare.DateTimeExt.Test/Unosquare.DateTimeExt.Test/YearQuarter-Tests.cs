namespace Unosquare.DateTimeExt.Test;

public class YearQuarterTests
{
    [Fact]
    public void WithQuarter_Deconstruct()
    {
        var yearQuarter = new YearQuarter(quarter: 1);
        yearQuarter.Deconstruct(out var startDate, out var endDate);

        Assert.Equal(new DateTime(2022, 1, 1), startDate);
        Assert.Equal(new DateTime(2022, 3, 31), endDate);
    }

    [Fact]
    public void WithYearQuarter_ReturnsFormattedString()
    {
        Assert.Equal("2022-Q1", new YearQuarter(year: 2022, quarter: 1).ToString());
    }

    [Fact]
    public void WithTwoQuarters_ComparesThem()
    {
        var yearQuarterOne = new YearQuarter(year: 2022, quarter: 1);
        var yearQuarterTwo = new YearQuarter(year: 2022, quarter: 2);

        Assert.Equal(-1, yearQuarterOne.CompareTo(yearQuarterTwo));
        Assert.Equal(0, yearQuarterOne.CompareTo(yearQuarterOne));
        Assert.Equal(1, yearQuarterTwo.CompareTo(yearQuarterOne));
    }
}
