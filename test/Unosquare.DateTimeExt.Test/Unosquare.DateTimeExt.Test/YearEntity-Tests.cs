namespace Unosquare.DateTimeExt.Test;

public class YearEntityTests
{
    [Fact]
    public void WithYear_ReturnsCounts()
    {
        var result = new YearEntity(2023);

        Assert.Equal(12, result.Months.Count);
        Assert.Equal(4, result.Quarters.Count);
        Assert.Equal(53, result.Weeks.Count);
    }
}