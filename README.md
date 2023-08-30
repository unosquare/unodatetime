[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=unosquare_unodatetime&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=unosquare_unodatetime)

# unodatetime

Unosquare DateTime

## Table of Contents
1. [DateExtensions](#DateExtensions)
2. [DateOnlyRange](#DateOnlyRange)
3. [DateRange](#DateRange)
4. [DateRangeRecord](#DateRangeRecord)
5. [MonthToDate](#MonthToDate)
6. [RangeBase](#RangeBase)
7. [TrailingTwelveMonths](#TrailingTwelveMonths)
8. [YearAbstract](#YearAbstract)
9. [YearEntity](#YearEntity)
10. [YearMonth](#YearMonth)
11. [YearMonthRecord](#YearMonthRecord)
12. [YearQuarter](#YearQuarter)
13. [YearQuarterRecord](#YearQuarterRecord)
14. [YearToDate](#YearToDate)
15. [YearWeek](#YearWeek)
16. [YearWeekRecord](#YearWeekRecord)

## DateExtensions
This class provides extension methods for DateTime objects.

Public Methods:
- ToFormattedString
- ToUtc
- GetQuarter
- ToDateOrNull
- GetWeekOfYear

Sample Code:
```csharp
DateTime date = DateTime.Now;
string formattedDate = date.ToFormattedString();
DateTime utcDate = date.ToUtc("EST");
int quarter = date.GetQuarter();
DateTime? nullableDate = "2022-01-01".ToDateOrNull();
int weekOfYear = date.GetWeekOfYear();