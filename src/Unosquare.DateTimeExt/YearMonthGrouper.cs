using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearMonthGrouper<T>(IEnumerable<T> query) : BaseGrouper<T, YearMonth> where T : IYearMonth
{
    public override IEnumerable<IGrouping<YearMonth, T>> GroupByDateRange() => query.GroupBy(x => new YearMonth(x.Month, x.Year));
}