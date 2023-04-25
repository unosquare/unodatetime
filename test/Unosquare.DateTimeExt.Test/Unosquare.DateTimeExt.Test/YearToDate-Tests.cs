namespace Unosquare.DateTimeExt.Test;

public class YearToDateTests
{
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
        Assert.Equal(new YearEntity(2020), new YearToDate(2020).YearEntity);
    }

    [Fact]
    public void WithYearToDateOnPast_ReturnsFormattedString()
    {
        Assert.Equal("YTD: 1/1/2020-12/31/2020", new YearToDate(2020).ToString());
    }
}