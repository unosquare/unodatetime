using Unosquare.DateTimeExt.Interfaces;

namespace Unosquare.DateTimeExt;

public class YearMonthRecordGrouper<T>(IEnumerable<T> query) : BaseGrouper<T, YearMonthRecord> where T : IYearMonth
{
    public override IEnumerable<IGrouping<YearMonthRecord, T>> GroupByDateRange() => query.GroupBy(x => new YearMonthRecord { Month = x.Month, Year = x.Year });
}