namespace Unosquare.DateTimeExt;

public sealed class YearEntity(int? year = null) : YearAbstract<YearEntity>(new(year ?? DateTime.UtcNow.Year, 1, 1),
    new(year ?? DateTime.UtcNow.Year, 12, 31))
{
    public YearEntity(DateTime? dateTime)
        : this((dateTime ?? DateTime.UtcNow).Year)
    {
    }

    public static YearEntity Current => new();

    public override YearEntity Next(int offset = 1) => new(StartDate.AddYears(offset));

    public override YearEntity Previous(int offset = 1) => new(StartDate.AddYears(-offset));

    public override bool IsCurrent => IsCurrentYear;
}
