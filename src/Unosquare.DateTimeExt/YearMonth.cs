using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearMonth : IYearMonth, IReadOnlyDateRange, IComparable<YearMonth>
{
    private readonly DateTime _startDate;

    public YearMonth(int? month = null, int? year = null) => _startDate = new(
        year ?? DateTime.UtcNow.Year,
        month ?? DateTime.UtcNow.Month,
        1);

    public YearMonth(DateTime dateTime)
        : this(dateTime.Month, dateTime.Year)
    {

    }

    public int Month => _startDate.Month;
    public int Year => _startDate.Year;

    public DateTime StartDate => _startDate;
    public DateTime EndDate => _startDate.GetLastDayOfMonth();

    public DateRange DateRange => new(_startDate, EndDate);

    public YearMonth Next => new(_startDate.AddMonths(1));

    public YearMonth Previous => new(_startDate.AddMonths(-1));

    public void Deconstruct(out DateTime startDate, out DateTime endDate)
    {
        startDate = StartDate;
        endDate = EndDate;
    }

    public void Deconstruct(out int year, out int month)
    {
        year = Year;
        month = Month;
    }

    public override string ToString() => $"{Year}-{Month}";

    public int CompareTo(YearMonth? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return ReferenceEquals(null, other) ? 1 : _startDate.CompareTo(other._startDate);
    }
}