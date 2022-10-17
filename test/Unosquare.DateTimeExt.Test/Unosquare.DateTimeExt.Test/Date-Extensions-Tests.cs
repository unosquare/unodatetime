namespace Unosquare.DateTimeExt.Test;

public class DateExtensionsTests
{
    [Fact]
    public void WithDate_ToFormattedString()
    {
        Assert.Equal("June 1, 2022", new DateTime(2022, 6, 1).ToFormattedString());
    }
}