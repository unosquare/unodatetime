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

    [Fact]
    public void WithYear_ReturnsFormattedString()
    {
        var result = new YearEntity(2023);

        Assert.Equal("1/1/2023 - 12/31/2023", result.ToString());
    }

    [Fact]
    public void WithYear_ReturnsNextYear()
    {
        var result = new YearEntity(2023);

        Assert.Equal(2024, result.Next.Year);
    }

    [Fact]
    public void WithYear_ReturnsPreviousYear()
    {
        var result = new YearEntity(2023);

        Assert.Equal(2022, result.Previous.Year);
    }

    [Fact]
    public void WithYear_ReturnsCurrentYear()
    {
        var result = YearEntity.Current;

        Assert.Equal(DateTime.Now.Year, result.Year);
    }
}