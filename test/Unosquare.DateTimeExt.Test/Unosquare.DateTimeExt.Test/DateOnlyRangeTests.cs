namespace Unosquare.DateTimeExt.Test;

public class DateOnlyRangeTests
{
    [Fact]
    public void DateOnlyRange_Default()
    {
        var dateRange = new DateOnlyRange();

        Assert.Equal(DateOnly.FromDateTime(DateTime.UtcNow), dateRange.StartDate);
    }

    [Fact]
    public void DateOnlyRange_InvalidRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new DateOnlyRange(new(2022, 10, 1), new(2022, 9, 30)));
    }

    [Fact]
    public void DateOnlyRange_FirstBusinessDay()
    {
        var dateRange = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        Assert.Equal(new(2022, 10, 3), dateRange.FirstBusinessDay);
    }

    [Fact]
    public void DateOnlyRange_LastBusinessDay()
    {
        var dateRange = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        Assert.Equal(new(2022, 10, 31), dateRange.LastBusinessDay);
    }

    [Fact]
    public void DateOnlyRange_Deconstruct()
    {
        var dateRange = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        dateRange.Deconstruct(out var startDate, out var endDate);

        Assert.Equal(new(2022, 10, 1), startDate);
        Assert.Equal(new(2022, 10, 31), endDate);
    }

    [Fact]
    public void WithYearQuarter_ReturnsFormattedString()
    {
        Assert.Equal("10/1/2022-10/31/2022",
            new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31)).ToString());
    }

    [Fact]
    public void WithDateRange_GetEnumerator()
    {
        using var dateRangeEnumerator = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31)).GetEnumerator();

        Assert.True(dateRangeEnumerator.MoveNext());
        Assert.Equal(new(2022, 10, 1), dateRangeEnumerator.Current);
        Assert.True(dateRangeEnumerator.MoveNext());
        Assert.Equal(new(2022, 10, 2), dateRangeEnumerator.Current);
    }

    [Fact]
    public void WithDateRanges_Equals()
    {
        var dateRange1 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        var dateRange2 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        var dateRange3 = new DateOnlyRange(new(2022, 10, 2), new(2022, 10, 31));

        Assert.True(dateRange1.Equals(dateRange2));
        Assert.False(dateRange1.Equals(dateRange3));
    }

    [Fact]
    public void WithTwoDateRanges_ComparesThem()
    {
        var dateRange1 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        var dateRange2 = new DateOnlyRange(new(2022, 10, 2), new(2022, 10, 31));

        Assert.Equal(-1, dateRange1.CompareTo(dateRange2));
        Assert.Equal(0, dateRange1.CompareTo(dateRange1));
        Assert.Equal(1, dateRange2.CompareTo(dateRange1));
    }

    [Fact]
    public void WithDateRanges_Select()
    {
        var dateRangeSelected = new DateOnlyRange(new(2022, 1, 1), new(2022, 1, 2)).Select(x => x);

        Assert.Equal(2, dateRangeSelected.Count());
    }

    [Fact]
    public void DateOnlyRange_EqualOperator()
    {
        var dateRange1 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        var dateRange2 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        var dateRange3 = new DateOnlyRange(new(2022, 10, 2), new(2022, 10, 31));

        Assert.True(dateRange1 == dateRange2);
        Assert.False(dateRange1 == dateRange3);
    }

    [Fact]
    public void DateOnlyRange_NotEqualOperator()
    {
        var dateRange1 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        var dateRange2 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        var dateRange3 = new DateOnlyRange(new(2022, 10, 2), new(2022, 10, 31));

        Assert.False(dateRange1 != dateRange2);
        Assert.True(dateRange1 != dateRange3);
    }

    [Fact]
    public void DateOnlyRange_LessThanOperator()
    {
        var dateRange1 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        var dateRange2 = new DateOnlyRange(new(2022, 10, 2), new(2022, 10, 31));

        Assert.True(dateRange1 < dateRange2);
    }

    [Fact]
    public void DateOnlyRange_LessThanOrEqualOperator()
    {
        var dateRange1 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));
        var dateRange2 = new DateOnlyRange(new(2022, 10, 2), new(2022, 10, 31));

        Assert.True(dateRange1 <= dateRange2);
    }

    [Fact]
    public void DateOnlyRange_GreaterThanOperator()
    {
        var dateRange1 = new DateOnlyRange(new(2022, 10, 2), new(2022, 10, 31));
        var dateRange2 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));

        Assert.True(dateRange1 > dateRange2);
    }

    [Fact]
    public void DateOnlyRange_GreaterThanOrEqualOperator()
    {
        var dateRange1 = new DateOnlyRange(new(2022, 10, 2), new(2022, 10, 31));
        var dateRange2 = new DateOnlyRange(new(2022, 10, 1), new(2022, 10, 31));

        Assert.True(dateRange1 >= dateRange2);
    }
}