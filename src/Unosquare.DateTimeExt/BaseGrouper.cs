using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public abstract class BaseGrouper<T, TConcrete> : IGrouper<T, TConcrete>
{
    public abstract IEnumerable<IGrouping<TConcrete, T>> GroupByDateRange();

    public IDictionary<string, List<T>> GroupByLabel() =>
        GroupByDateRange()
            .ToDictionary(x => x.Key!.ToString()!, x => x.ToList());

    public IDictionary<string, List<TResult>> GroupByLabel<TResult>(Func<T, TResult> selector) =>
        GroupByDateRange()
            .ToDictionary(x => x.Key!.ToString()!, x => x.Select(selector).ToList());

    public IDictionary<string, int> GroupCount() =>
        GroupByDateRange()
            .ToDictionary(x => x.Key!.ToString()!, x => x.Count());
}