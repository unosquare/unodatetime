namespace Unosquare.DateTimeExt.Interfaces;

public interface IGrouper<T, out TConcrete>
{
    IEnumerable<IGrouping<TConcrete, T>> GroupByDateRange();

    IDictionary<string, List<T>> GroupByLabel();

    IDictionary<string, List<TResult>> GroupByLabel<TResult>(Func<T, TResult> selector);

    IDictionary<string, int> GroupCount();
}