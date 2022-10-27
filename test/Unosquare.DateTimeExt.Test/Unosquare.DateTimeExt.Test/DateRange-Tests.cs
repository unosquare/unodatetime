namespace Unosquare.DateTimeExt.Test;

public class DateRangeTests
{
    [Fact]
    public void WithQuarter_Deconstruct()
    {
        var dateRange = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        dateRange.Deconstruct(out var startDate, out var endDate);

        Assert.Equal(new(2022, 10, 1), startDate);
        Assert.Equal(new(2022, 10, 31), endDate);
    }

    [Fact]
    public void WithYearQuarter_ReturnsFormattedString()
    {
        Assert.Equal("10/1/2022-10/31/2022",
            new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31)).ToString());
    }

    [Fact]
    public void WithDateRange_GetEnumerator()
    {
        var dateRangeEnumerator = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31)).GetEnumerator();

        Assert.True(dateRangeEnumerator.MoveNext());
        Assert.Equal(new(2022, 10, 1), dateRangeEnumerator.Current);
        Assert.True(dateRangeEnumerator.MoveNext());
        Assert.Equal(new(2022, 10, 2), dateRangeEnumerator.Current);
    }

    [Fact]
    public void WithDateRanges_Equals()
    {
        var dateRange1 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange2 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange3 = new DateRange(new(2022, 10, 2), new DateTime(2022, 10, 31));

        Assert.True(dateRange1.Equals(dateRange2));
        Assert.False(dateRange1.Equals(dateRange3));
    }

    [Fact]
    public void WithTwoDateRanges_ComparesThem()
    {
        var dateRange1 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange2 = new DateRange(new(2022, 10, 2), new DateTime(2022, 10, 31));

        Assert.Equal(-1, dateRange1.CompareTo(dateRange2));
        Assert.Equal(0, dateRange1.CompareTo(dateRange1));
        Assert.Equal(1, dateRange2.CompareTo(dateRange1));
    }

    [Fact]
    public void WithDateRanges_Select()
    {
        var dateRangeSelected = new DateRange(new(2022, 1, 1), new DateTime(2022, 1, 2)).Select(x => x);

        Assert.Equal(2, dateRangeSelected.Count());
    }

}