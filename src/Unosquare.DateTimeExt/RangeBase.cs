namespace Unosquare.DateTimeExt;

public abstract class RangeBase<TStart, TEnd> where TStart : struct
{
    protected RangeBase(TStart startDate, TEnd endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public TStart StartDate { get; }

    public TEnd EndDate { get; }

    public IEnumerable<TK> Select<TK>(Func<TStart, TK> selector)
    {
        using var enumerator = GetEnumerator();

        while (enumerator.MoveNext())
            yield return selector(enumerator.Current);
    }

    public abstract IEnumerator<TStart> GetEnumerator();

    public void Deconstruct(out TStart startDate, out TEnd endDate)
    {
        startDate = StartDate;
        endDate = EndDate;
    }
}