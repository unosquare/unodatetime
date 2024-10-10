namespace Unosquare.DateTimeExt;

public abstract class RangeBase<TStart, TEnd>(TStart startDate, TEnd endDate)
    where TStart : struct
{
    public TStart StartDate { get; } = startDate;

    public TEnd EndDate { get; } = endDate;

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