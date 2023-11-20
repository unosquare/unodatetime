namespace Unosquare.DateTimeExt.Test;

public class OpenDateRangeTests
{
    [Fact]
    public void GetEnumerator_ShouldReturnAllDatesInRange_WhenEndDateIsNotNull()
    {
        // Arrange
        var startDate = new DateTime(2022, 1, 1);
        var endDate = new DateTime(2022, 1, 5);
        var openDateRange = new OpenDateRange(startDate, endDate);
        var expectedDates = new List<DateTime>
        {
            new(2022, 1, 1),
            new(2022, 1, 2),
            new(2022, 1, 3),
            new(2022, 1, 4),
            new(2022, 1, 5)
        };

        // Act
        var actualDates = new List<DateTime>(openDateRange);

        // Assert
        Assert.Equal(expectedDates, actualDates);
    }

    [Fact]
    public void GetEnumerator_ShouldReturnAllDatesFromStartDateToMaxValue_WhenEndDateIsNull()
    {
        // Arrange
        var startDate = new DateTime(2022, 1, 1);
        var openDateRange = new OpenDateRange(startDate);
        var expectedDates = new List<DateTime>
        {
            new(2022, 1, 2),
            new(2022, 1, 3),
        };

        // Act
        var actualDates = openDateRange.Take(2).ToList();

        // Assert
        Assert.Equal(expectedDates, actualDates);
    }

    [Fact]
    public void CompareTo_ShouldReturnNegativeValue_WhenStartDateIsEarlier()
    {
        // Arrange
        var startDate1 = new DateTime(2022, 1, 1);
        var startDate2 = new DateTime(2022, 1, 2);
        var openDateRange1 = new OpenDateRange(startDate1);
        var openDateRange2 = new OpenDateRange(startDate2);

        // Act
        var result = openDateRange1.CompareTo(openDateRange2);

        // Assert
        Assert.True(result < 0);
    }

    [Fact]
    public void CompareTo_ShouldReturnPositiveValue_WhenStartDateIsLater()
    {
        // Arrange
        var startDate1 = new DateTime(2022, 1, 2);
        var startDate2 = new DateTime(2022, 1, 1);
        var openDateRange1 = new OpenDateRange(startDate1);
        var openDateRange2 = new OpenDateRange(startDate2);

        // Act
        var result = openDateRange1.CompareTo(openDateRange2);

        // Assert
        Assert.True(result > 0);
    }

    [Fact]
    public void CompareTo_ShouldReturnZero_WhenStartDateAndEndDateAreEqual()
    {
        // Arrange
        var startDate1 = new DateTime(2022, 1, 1);
        var startDate2 = new DateTime(2022, 1, 1);
        var openDateRange1 = new OpenDateRange(startDate1);
        var openDateRange2 = new OpenDateRange(startDate2);

        // Act
        var result = openDateRange1.CompareTo(openDateRange2);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString_WhenEndDateIsNotNull()
    {
        // Arrange
        var startDate = new DateTime(2022, 1, 1);
        var endDate = new DateTime(2022, 1, 5);
        var openDateRange = new OpenDateRange(startDate, endDate);
        const string expectedString = "1/1/2022 - 1/5/2022";

        // Act
        var actualString = openDateRange.ToString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString_WhenEndDateIsNull()
    {
        // Arrange
        var startDate = new DateTime(2022, 1, 1);
        var openDateRange = new OpenDateRange(startDate);
        const string expectedString = "1/1/2022 - ";

        // Act
        var actualString = openDateRange.ToString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenStartDateAndEndDateAreEqual()
    {
        // Arrange
        var startDate1 = new DateTime(2022, 1, 1);
        var startDate2 = new DateTime(2022, 1, 1);
        var openDateRange1 = new OpenDateRange(startDate1);
        var openDateRange2 = new OpenDateRange(startDate2);

        // Act
        var hashCode1 = openDateRange1.GetHashCode();
        var hashCode2 = openDateRange2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void FirstBusinessDay_ShouldReturnFirstBusinessDayOfMonth()
    {
        // Arrange
        var startDate = new DateTime(2022, 1, 1);
        var openDateRange = new OpenDateRange(startDate);
        var expectedFirstBusinessDay = new DateTime(2022, 1, 3);

        // Act
        var actualFirstBusinessDay = openDateRange.FirstBusinessDay;

        // Assert
        Assert.Equal(expectedFirstBusinessDay, actualFirstBusinessDay);
    }

    [Fact]
    public void LastBusinessDay_ShouldReturnLastBusinessDayOfMonth_WhenEndDateIsNotNull()
    {
        // Arrange
        var startDate = new DateTime(2022, 1, 1);
        var endDate = new DateTime(2022, 1, 31);
        var openDateRange = new OpenDateRange(startDate, endDate);
        var expectedLastBusinessDay = new DateTime(2022, 1, 31);

        // Act
        var actualLastBusinessDay = openDateRange.LastBusinessDay;

        // Assert
        Assert.Equal(expectedLastBusinessDay, actualLastBusinessDay);
    }

    [Fact]
    public void LastBusinessDay_ShouldReturnLastBusinessDayOfMonth_WhenEndDateIsNull()
    {
        // Arrange
        var startDate = new DateTime(2022, 1, 1);
        var openDateRange = new OpenDateRange(startDate);
        var expectedLastBusinessDay = DateTime.Now.GetLastBusinessDayOfMonth();

        // Act
        var actualLastBusinessDay = openDateRange.LastBusinessDay;

        // Assert
        Assert.Equal(expectedLastBusinessDay, actualLastBusinessDay);
    }
}