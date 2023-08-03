namespace Unosquare.DateTimeExt.Test;

public class YearQuarterTests
{
    [Fact]
    public void WithQuarter_Deconstruct()
    {
        var yearQuarter = new YearQuarter(quarter: 1);
        yearQuarter.Deconstruct(out DateTime startDate, out var endDate);

        Assert.Equal(new(DateTime.UtcNow.Year, 1, 1), startDate);
        Assert.Equal(new(DateTime.UtcNow.Year, 3, 31), endDate);
    }

    [Fact]
    public void WithYearQuarter_ReturnsFormattedString()
    {
        Assert.Equal("2022-Q1", new YearQuarter(year: 2022, quarter: 1).ToString());
    }

    [Fact]
    public void WithYearQuarter_ReturnsMonths()
    {
        Assert.Equal(new[] { 10, 11, 12 }, new YearQuarter(year: 2022, quarter: 4).Months);
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

    [Fact]
    public void WithYearQuarterCurrent_IsCurrentTrue()
    {
        Assert.True(YearQuarter.Current.IsCurrent);
    }

    [Fact]
    public void WithYearQuarterCurrent_IsCurrentFalse()
    {
        Assert.False(new YearQuarter(year: 2022, quarter: 1).IsCurrent);
    }

    [Fact]
    public void WithYearQuarterAddQuarters_AddsQuarters()
    {
        Assert.Equal(new(year: 2022, quarter: 2), new YearQuarter(year: 2022, quarter: 1).AddQuarters(1));
    }

    [Fact]
    public void WithYearQuarterAddQuarters_SubtractsQuarters()
    {
        Assert.Equal(new(year: 2022, quarter: 1), new YearQuarter(year: 2022, quarter: 2).AddQuarters(-1));
    }

    [Fact]
    public void WithYearQuarterYearEntity_ReturnsYearEntity()
    {
        Assert.Equal(new(year: 2022), new YearQuarter(year: 2022, quarter: 1).YearEntity);
    }

    [Fact]
    public void WithYearQuarter_TryParse_ReturnsValidDate()
    {
        var result = YearQuarter.TryParse("2021-Q4", out YearQuarter quarter);
        var expectedQuarter = new YearQuarter(4, 2021);

        Assert.True(result);
        Assert.Equal(expectedQuarter, quarter);
    }

    [Fact]
    public void WithYearQuarter_TryParse_ReturnsInvalidDate()
    {
        bool result = YearQuarter.TryParse("YearQuarter", out _);
        Assert.False(result);
    }
}
