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
}