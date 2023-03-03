namespace Unosquare.DateTimeExt;

public sealed class YearEntity : YearAbstract
{
    public YearEntity(int? year = null)
        : base(new(year ?? DateTime.UtcNow.Year, 1, 1), new(year ?? DateTime.UtcNow.Year, 12, 31))
    {
    }

    public YearEntity(DateTime? dateTime)
        : this((dateTime ?? DateTime.UtcNow).Year)
    {
    }

    public static YearEntity Current => new();
}