using System.Collections;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class DateRange : IReadOnlyDateRange, IHasReadOnlyMidnightEndDate, IComparable<DateRange>, IEnumerable<DateTime>, IHasBusinessDays
{
    public DateRange()
        : this(DateTime.UtcNow)
    {
    }

    public DateRange(DateTime startDate, DateTime? endDate = null)
    {
        StartDate = startDate;
        EndDate = endDate ?? startDate;

        if (EndDate < StartDate)
            throw new ArgumentOutOfRangeException(nameof(EndDate), "End Date should be after Start Date");

    }

    public DateTime StartDate { get; }

    public DateTime EndDate { get; }

    public DateTime MidnightEndDate => EndDate.Date.AddDays(1).AddSeconds(-1);

    public DateTime FirstBusinessDay => StartDate.GetFirstBusinessDayOfMonth();

    public DateTime LastBusinessDay => EndDate.GetLastBusinessDayOfMonth();

    public int DaysInBetween => (EndDate - StartDate).Days;

    public void Deconstruct(out DateTime startDate, out DateTime endDate)
    {
        startDate = StartDate;
        endDate = EndDate;
    }

    public override string ToString() => $"{StartDate.ToShortDateString()}-{EndDate.ToShortDateString()}";

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override int GetHashCode() => StartDate.GetHashCode() + EndDate.GetHashCode();

    public IEnumerator<DateTime> GetEnumerator()
    {
        for (var i = 0; i <= DaysInBetween; i++)
            yield return StartDate.AddDays(i);
    }

    public IEnumerable<TK> Select<TK>(Func<DateTime, TK> selector)
    {
        using var enumerator = GetEnumerator();

        while (enumerator.MoveNext())
            yield return selector(enumerator.Current);
    }

    public override bool Equals(object? obj) =>
        (obj is DateRange other) && other.StartDate == StartDate && other.EndDate == EndDate;

    public int CompareTo(DateRange? other)
    {
        if (ReferenceEquals(this, other))
            return 0;

        if (other is null)
            return 1;

        var startDateComparison = StartDate.CompareTo(other.StartDate);

        return startDateComparison != 0 ? startDateComparison : EndDate.CompareTo(other.EndDate);
    }

    public static bool operator ==(DateRange? left, DateRange? right) => left?.Equals(right) ?? right is null;

    public static bool operator >(DateRange left, DateRange right) => left.StartDate > right.StartDate;

    public static bool operator <(DateRange left, DateRange right) => left.EndDate < right.EndDate;

    public static bool operator !=(DateRange left, DateRange right) => !(left == right);
}