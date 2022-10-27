namespace Unosquare.DateTimeExt.Test;

public class YearWeekTests
{
    [Fact]
    public void WithWeek_Deconstruct()
    {
        var yearMonth = new YearWeek(1, 2022);
        yearMonth.Deconstruct(out DateTime startDate, out var endDate);

        Assert.Equal(new(2022, 1, 2), startDate);
        Assert.Equal(new(2022, 1, 8), endDate);
    }
}