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
}