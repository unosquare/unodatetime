using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearQuarterRecordGrouper<T>(IEnumerable<T> query) : BaseGrouper<T, YearQuarterRecord> where T : IYearQuarter
{
    public override IEnumerable<IGrouping<YearQuarterRecord, T>> GroupByDateRange() => query.GroupBy(x => new YearQuarterRecord { Year = x.Year, Quarter = x.Quarter });
}