using System.Collections;
using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class DateRange : IReadOnlyDateRange, IHasReadOnlyMidnightEndDate, IComparable<DateRange>, IEnumerable<DateTime>
{
    public DateRange()
        : this(DateTime.UtcNow)
    {
    }

    public DateRange(DateTime startDate, DateTime? endDate = null)
    {
        StartDate = startDate;
        EndDate = endDate ?? startDate;
    }

    public DateTime StartDate { get; }

    public DateTime EndDate { get; }

    public DateTime MidnightEndDate => EndDate.Date.AddDays(1).AddSeconds(-1);

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
        var daysDifference = (EndDate - StartDate).Days;

        for (var i = 0; i <= daysDifference; i++)
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
}