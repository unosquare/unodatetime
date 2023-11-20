using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearQuarterGrouper<T>(IEnumerable<T> query) : BaseGrouper<T, YearQuarter> where T : IYearQuarter
{
    public override IEnumerable<IGrouping<YearQuarter, T>> GroupByDateRange() => query.GroupBy(x => new YearQuarter(x.Quarter, x.Year));
}