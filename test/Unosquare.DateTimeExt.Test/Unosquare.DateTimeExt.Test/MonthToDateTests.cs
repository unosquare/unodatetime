namespace Unosquare.DateTimeExt.Test;

public class MonthToDateTests
{
    [Fact]
    public void MonthToDateConstructorTest()
    {
        var mtd = new MonthToDate(1, 2021);
        Assert.Equal(1, mtd.Month);
        Assert.Equal(2021, mtd.Year);
    }

    [Fact]
    public void MonthToDateCurrentTest()
    {
        var mtd = new MonthToDate();
        Assert.Equal(DateTime.Today, mtd.EndDate);
    }

    [Fact]
    public void MonthToDateFromYearMonth()
    {
        var yearMonth = new YearMonth(1, 2021);
        var mtd = new MonthToDate(yearMonth);
        Assert.Equal(1, mtd.Month);
        Assert.Equal(2021, mtd.Year);
    }

    [Fact]
    public void MonthToDateFromDate()
    {
        var yearMonth = new DateTime(2021, 1, 1);
        var mtd = new MonthToDate(yearMonth);
        Assert.Equal(1, mtd.Month);
        Assert.Equal(2021, mtd.Year);
    }

    [Fact]
    public void MonthToDateEndTest()
    {
        var mtd = new MonthToDate(1, 2021);
        Assert.Equal(new(2021, 1, 31), mtd.EndDate);
    }

    [Fact]
    public void MonthToDateStartTest()
    {
        var mtd = new MonthToDate(1, 2021);
        Assert.Equal(new(2021, 1, 1), mtd.StartDate);
    }

    [Fact]
    public void MonthToDateToString()
    {
        var mtd = new MonthToDate(1, 2021);
        Assert.Equal("MTD: 2021-01", mtd.ToString());
    }
}