namespace Unosquare.DateTimeExt.Test;

public class YearToDateTests
{
    [Fact]
    public void YearToDate_FromCurrent()
    {
        var ytd = new YearToDate(YearEntity.Current);
        Assert.Equal(DateTime.UtcNow.Date, ytd.EndDate);
    }

    [Fact]
    public void YearToDateFromYear()
    {
        var ytd = new YearToDate(2021);
        Assert.Equal(2021, ytd.Year);
    }

    [Fact]
    public void WithYearToDateOnPast_ReturnsDecember()
    {
        Assert.Equal(new(2020, 12, 31), new YearToDate(2020).EndDate);
    }

    [Fact]
    public void WithYearToDateOnPast_ReturnsJanuary()
    {
        Assert.Equal(new(2020, 1, 1), new YearToDate(2020).StartDate);
    }

    [Fact]
    public void WithYearToDateOnCurrent_ReturnsNow()
    {
        Assert.Equal(DateTime.UtcNow.Date, new YearToDate().EndDate);
    }

    [Fact]
    public void WithYearToDateOnPast_ReturnsYear()
    {
        Assert.Equal(2020, new YearToDate(2020).Year);
    }

    [Fact]
    public void WithYearToDateOnPast_ReturnsAllMonths()
    {
        Assert.Equal(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, new YearToDate(2020).Months);
    }

    [Fact]
    public void WithYearToDateOnPast_ReturnsYearEntity()
    {
        Assert.Equal(new(2020), new YearToDate(2020).YearEntity);
    }

    [Fact]
    public void WithYearToDateOnPast_ReturnsMonth()
    {
        Assert.Equal(12, new YearToDate(2020).Month);
    }

    [Fact]
    public void WithYearToDateOnPast_ReturnsFormattedString()
    {
        Assert.Equal("YTD: 1/1/2020 - 12/31/2020", new YearToDate(2020).ToString());
    }

    [Fact]
    public void YearToDateCurrent_ReturnsYear()
    {
        Assert.Equal(DateTime.UtcNow.Year, YearToDate.Current.Year);
    }

    [Fact]
    public void YearToDate_MonthToDate()
    {
        var yearToDate = new YearToDate();
        var monthToDate = yearToDate.MonthToDate;

        Assert.Equal(yearToDate.Year, monthToDate.Year);
        Assert.Equal(yearToDate.StartDate.Month, monthToDate.Month);
    }

    [Fact]
    public void YearToDate_YearMonth()
    {
        var yearToDate = new YearToDate();
        var yearMonth = yearToDate.YearMonth;

        Assert.Equal(yearToDate.Year, yearMonth.Year);
        Assert.Equal(yearToDate.StartDate.Month, yearMonth.Month);
    }

    [Fact]
    public void YearToDate_YearQuarters()
    {
        var yearToDate = new YearToDate(2021);
        var yearQuarters = yearToDate.YearQuarters;

        Assert.Equal(4, yearQuarters.Count);
        Assert.Equal(new(2021, 1, 1), yearQuarters.First().StartDate);
    }

    [Fact]
    public void YearToDate_YearMonths()
    {
        var yearToDate = new YearToDate(2021);
        var yearMonths = yearToDate.YearMonths;

        Assert.Equal(12, yearMonths.Count);
        Assert.Equal(new(2021, 1, 1), yearMonths.First().StartDate);
    }
}