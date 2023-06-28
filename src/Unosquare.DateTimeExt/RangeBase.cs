namespace Unosquare.DateTimeExt;

public abstract class RangeBase<T> where T : struct
{
    protected RangeBase(T startDate, T? endDate = default)
    {
        StartDate = startDate;
        EndDate = endDate ?? startDate;
    }

    public T StartDate { get; }

    public T EndDate { get; }

    public IEnumerable<TK> Select<TK>(Func<T, TK> selector)
    {
        using var enumerator = GetEnumerator();

        while (enumerator.MoveNext())
            yield return selector(enumerator.Current);
    }

    public abstract IEnumerator<T> GetEnumerator();

    public void Deconstruct(out T startDate, out T endDate)
    {
        startDate = StartDate;
        endDate = EndDate;
    }
}