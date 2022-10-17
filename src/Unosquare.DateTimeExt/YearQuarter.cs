using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearQuarter : IYearQuarter, IReadOnlyDateRange, IComparable<YearQuarter>
{
    private readonly DateTime _startDate;

    public YearQuarter(int? quarter = null, int? year = null) => _startDate = new(
        year ?? DateTime.UtcNow.Year,
        ((quarter ?? DateTime.UtcNow.GetQuarter()) - 1) * 3 + 1,
        1);

    public YearQuarter(DateTime dateTime)
        : this(dateTime.GetQuarter(), dateTime.Year)
    {

    }

    public int Quarter => _startDate.GetQuarter();
    public int Year => _startDate.Year;

    public DateTime StartDate => _startDate;
    public DateTime EndDate => _startDate.AddMonths(3).AddDays(-1);

    public DateRange DateRange => new(_startDate, EndDate);

    public YearQuarter Next => new(_startDate.AddMonths(3));

    public YearQuarter Previous => new(_startDate.AddMonths(-3));

    public void Deconstruct(out DateTime startDate, out DateTime endDate)
    {
        startDate = StartDate;
        endDate = EndDate;
    }

    public override string ToString() => $"{Year}-Q{Quarter}";

    public int CompareTo(YearQuarter? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        return ReferenceEquals(null, other) ? 1 : _startDate.CompareTo(other._startDate);
    }
}