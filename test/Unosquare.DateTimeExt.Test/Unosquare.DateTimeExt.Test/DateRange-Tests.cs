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
        Assert.Equal("10/1/2022 - 10/31/2022",
            new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31)).ToString());
    }

    [Fact]
    public void WithDateRange_GetEnumerator()
    {
        using var dateRangeEnumerator = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31)).GetEnumerator();

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

    [Fact]
    public void WithDateRanges_EqualOperator()
    {
        var dateRange1 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange2 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange3 = new DateRange(new(2022, 10, 2), new DateTime(2022, 10, 31));

        Assert.True(dateRange1 == dateRange2);
        Assert.False(dateRange1 == dateRange3);
    }

    [Fact]
    public void WithDateRanges_NotEqualOperator()
    {
        var dateRange1 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange2 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange3 = new DateRange(new(2022, 10, 2), new DateTime(2022, 10, 31));

        Assert.False(dateRange1 != dateRange2);
        Assert.True(dateRange1 != dateRange3);
    }

    [Fact]
    public void WithDateRange_GreatThanOperator()
    {
        var dateRange1 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange2 = new DateRange(new(2022, 10, 2), new DateTime(2022, 10, 31));

        Assert.True(dateRange2 > dateRange1);
    }

    [Fact]
    public void WithDateRange_LessThanOperator()
    {
        var dateRange1 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange2 = new DateRange(new(2022, 10, 2), new DateTime(2022, 10, 31));

        Assert.True(dateRange1 < dateRange2);
    }

    [Fact]
    public void WithDateRange_GreatThanOrEqualOperator()
    {
        var dateRange1 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange2 = new DateRange(new(2022, 10, 2), new DateTime(2022, 10, 31));

        Assert.True(dateRange2 >= dateRange1);
    }

    [Fact]
    public void WithDateRange_LessThanOrEqualOperator()
    {
        var dateRange1 = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var dateRange2 = new DateRange(new(2022, 10, 2), new DateTime(2022, 10, 31));

        Assert.True(dateRange1 <= dateRange2);
    }
    [Fact]
    public void WithDateRange_FirstBusinessDay()
    {
        var dateRange = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));

        Assert.Equal(new(2022, 10, 3), dateRange.FirstBusinessDay);
    }

    [Fact]
    public void WithDateRange_LastBusinessDay()
    {
        var dateRange = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));

        Assert.Equal(new(2022, 10, 31), dateRange.LastBusinessDay);
    }

    [Theory]
    [InlineData(10, 1)]
    [InlineData(10, 15)]
    [InlineData(10, 31)]
    public void WithDateTime_Contains_TrueCases(params int[] values)
    {
        var dateRange = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var date = new DateTime(2022, values[0], values[1]);

        Assert.True(dateRange.Contains(date));
    }

    [Theory]
    [InlineData(9, 30)]
    [InlineData(11, 1)]
    public void WithDateTime_Contains_FalseCases(params int[] values)
    {
        var dateRange = new DateRange(new(2022, 10, 1), new DateTime(2022, 10, 31));
        var date = new DateTime(2022, values[0], values[1]);

        Assert.False(dateRange.Contains(date));
    }

    [Fact]
    public void WithString_ParseCorrectValue()
    {
        const string dateString = "2022-10-01 - 2022-10-31";

        Assert.True(DateRange.TryParse(dateString, out var dateRange));
        Assert.Equal(new(2022, 10, 1), dateRange.StartDate);
        Assert.Equal(new(2022, 10, 31), dateRange.EndDate);
    }

    [Fact]
    public void WithString_ParseIncorrectValue()
    {
        const string dateString = "2022-10-01 - 2022-10-32";

        Assert.False(DateRange.TryParse(dateString, out _));
    }

    [Fact]
    public void WithString_ParseIncorrectValue2()
    {
        const string dateString = "2022-10-32 - 2022-10-31";

        Assert.False(DateRange.TryParse(dateString, out _));
    }

    [Fact]
    public void WithString_ParseIncorrectEmpty()
    {
        const string dateString = "";

        Assert.False(DateRange.TryParse(dateString, out _));
    }
}