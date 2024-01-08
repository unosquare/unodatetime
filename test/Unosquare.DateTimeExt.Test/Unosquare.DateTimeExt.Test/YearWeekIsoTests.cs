namespace Unosquare.DateTimeExt.Test;

public class YearWeekIsoTests
{
    [Fact]
    public void WithWeek_Deconstruct()
    {
        var yearMonth = new YearWeekIso(1, 2022);
        yearMonth.Deconstruct(out DateTime startDate, out var endDate);

        Assert.Equal(new(2022, 1, 3), startDate);
        Assert.Equal(new DateTime(2022, 1, 9).ToMidnight(), endDate);
    }

    [Fact]
    public void WithWeek_FromDateTime()
    {
        var yearMonth = new YearWeekIso(new DateTime(2022, 1, 2));

        Assert.Equal(52, yearMonth.Week);
        Assert.Equal(2021, yearMonth.Year);
    }

    [Fact]
    public void WithWeek_DateRange()
    {
        var yearMonth = new YearWeekIso(1, 2022);

        Assert.Equal(new(2022, 1, 3), yearMonth.StartDate);
        Assert.Equal(new DateTime(2022, 1, 9).ToMidnight(), yearMonth.EndDate);
    }

    [Fact]
    public void WithWeek_AddWeeksNegative()
    {
        var yearMonth = new YearWeekIso(2, 2022);

        Assert.Equal(new(2022, 1, 3), yearMonth.AddWeeks(-1).StartDate);
    }

    [Fact]
    public void WithWeek_AddWeeksPositive()
    {
        var yearMonth = new YearWeekIso(1, 2022);

        Assert.Equal(new(2022, 1, 10), yearMonth.AddWeeks(1).StartDate);
    }

    [Fact]
    public void WithWeek_Previous()
    {
        var yearMonth = new YearWeekIso(2, 2022);

        Assert.Equal(new(2022, 1, 3), yearMonth.Previous.StartDate);
    }

    [Fact]
    public void WithWeek_Next()
    {
        var yearMonth = new YearWeekIso(1, 2022);

        Assert.Equal(new(2022, 1, 10), yearMonth.Next.StartDate);
    }

    [Fact]
    public void WithWeek_IsCurrent()
    {
        var yearMonth = new YearWeekIso();

        Assert.True(yearMonth.IsCurrent);
    }

    [Fact]
    public void WithWeek_DeconstructYearMonth()
    {
        var yearMonth = new YearWeekIso(1, 2022);
        yearMonth.Deconstruct(out int year, out int week);

        Assert.Equal(1, week);
        Assert.Equal(2022, year);
    }

    [Fact]
    public void TryParse_Valid_WeekYearString_ReturnsTrue()
    {
        // Arrange
        const string input = "2022-W01";
        YearWeekIso expected = new(1, 2022);

        // Act
        bool result = YearWeekIso.TryParse(input, out var actual);

        // Assert
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TryParse_Invalid_WeekYearString_ReturnsFalse()
    {
        // Arrange
        const string input = "not a valid input string";

        // Act
        bool result = YearWeekIso.TryParse(input, out var actual);

        // Assert
        Assert.False(result);
        Assert.Null(actual);
    }

    [Fact]
    public void YearWeekIso_Current()
    {
        var yearWeek = YearWeekIso.Current;

        Assert.Equal(DateTime.UtcNow.Month == 1 && DateTime.UtcNow.GetWeekOfYear() > 5
            ? DateTime.UtcNow.Year - 1
            : DateTime.UtcNow.Year, yearWeek.Year);
    }

    [Fact]
    public void YearWeekIso_BoWYearEntity()
    {
        var yearWeek = new YearWeekIso(1, 2022);
        var yearWeekBoW = yearWeek.BoWYearEntity;

        Assert.Equal(yearWeek.Year, yearWeekBoW.Year);
        Assert.Equal(yearWeek.StartDate.Month, yearWeekBoW.StartDate.Month);
        Assert.Equal(1, yearWeekBoW.StartDate.Day);
    }

    [Fact]
    public void YearWeekIso_EoWYearEntity()
    {
        var yearWeek = new YearWeekIso(1, 2022);
        var yearWeekBoW = yearWeek.EoWYearEntity;

        Assert.Equal(yearWeek.Year, yearWeekBoW.Year);
        Assert.Equal(yearWeek.StartDate.Month, yearWeekBoW.StartDate.Month);
        Assert.Equal(1, yearWeekBoW.StartDate.Day);
    }

    [Fact]
    public void YearWeekIso_ToRecord()
    {
        var yearWeek = new YearWeekIso(1, 2022);
        var record = yearWeek.ToRecord();

        Assert.Equal(yearWeek.Year, record.Year);
        Assert.Equal(yearWeek.StartDate.Month, record.Week);
    }

    [Fact]
    public void YearWeekIso_Equals()
    {
        var yearWeek1 = new YearWeekIso(1, 2022);
        var yearWeek2 = new YearWeekIso(1, 2022);
        var yearWeek3 = new YearWeekIso(2, 2022);

        Assert.True(yearWeek1.Equals(yearWeek2));
        Assert.False(yearWeek1.Equals(yearWeek3));
    }

    [Fact]
    public void YearWeekIso_CompareTo()
    {
        var yearWeek1 = new YearWeekIso(1, 2022);
        var yearWeek2 = new YearWeekIso(1, 2022);
        var yearWeek3 = new YearWeekIso(2, 2022);

        Assert.Equal(0, yearWeek1.CompareTo(yearWeek2));
        Assert.Equal(-1, yearWeek1.CompareTo(yearWeek3));
        Assert.Equal(1, yearWeek3.CompareTo(yearWeek1));
    }
}