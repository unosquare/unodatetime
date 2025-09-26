# Unosquare DateTime Extensions

[![Build](https://github.com/unosquare/unodatetime/actions/workflows/build.yml/badge.svg)](https://github.com/unosquare/unodatetime/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/v/Unosquare.DateTimeExt.svg)](https://www.nuget.org/packages/Unosquare.DateTimeExt)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

A comprehensive C# library providing powerful DateTime helper classes and extension methods for .NET 9.0. This library simplifies working with dates, date ranges, business days, quarters, weeks, and time periods commonly used in business applications.

## Features

### 📅 DateTime Extensions
- **Date Formatting**: Format dates consistently across your application
- **Timezone Conversion**: Convert dates to UTC with timezone support  
- **Business Days**: Calculate business days, get first/last business day of month
- **Quarter Operations**: Get quarters, calculate quarter-based periods
- **Week Operations**: ISO week support, week of year calculations
- **Date Utilities**: Get first/last day of month, weekend detection, midnight conversion

### 📊 Date Range Classes
- **DateRange**: Comprehensive date range with enumeration and comparison support
- **DateOnlyRange**: Date-only ranges without time components
- **OpenDateRange**: Ranges with optional end dates for ongoing periods
- **WorkingDateRange**: Business-focused ranges with working hours calculations

### 🗓️ Time Period Classes
- **YearMonth**: Year-month combinations with full date range functionality
- **YearQuarter**: Quarterly periods with business logic
- **YearWeek** & **YearWeekIso**: Week-based periods (standard and ISO formats)
- **YearEntity**: Full year representations with comprehensive period access
- **YearToDate**: Current year up to a specific date
- **MonthToDate**: Current month up to a specific date  
- **TrailingTwelveMonths**: Rolling 12-month periods for financial analysis

### 🔧 Grouping and Analysis Tools
- **Grouper Classes**: Group data by year-month, year-quarter, or year-week periods
- **Record Types**: Lightweight data transfer objects for period representations
- **Interface System**: Comprehensive interfaces for consistent API design

## Installation

```bash
Install-Package Unosquare.DateTimeExt
```

Or via .NET CLI:
```bash
dotnet add package Unosquare.DateTimeExt
```

## Quick Start

```csharp
using Unosquare.DateTimeExt;

// DateTime Extensions
var date = new DateTime(2025, 6, 15);
Console.WriteLine(date.ToFormattedString()); // "June 15, 2025"
Console.WriteLine(date.GetQuarter()); // 2
Console.WriteLine(date.IsWeekend()); // false
Console.WriteLine(date.GetBusinessDays(new DateTime(2025, 6, 20))); // 4

// Date Ranges
var range = new DateRange(new DateTime(2025, 1, 1), new DateTime(2025, 1, 31));
Console.WriteLine(range.DaysInBetween); // 30
Console.WriteLine(range.Contains(new DateTime(2025, 1, 15))); // true

// Time Periods
var yearMonth = new YearMonth(6, 2025); // June 2025
var quarter = new YearQuarter(2, 2025); // Q2 2025
var yearToDate = YearToDate.Current;

// Business Logic
Console.WriteLine(date.GetFirstBusinessDayOfMonth()); // First business day of June 2025
Console.WriteLine(date.GetLastBusinessDayOfMonth()); // Last business day of June 2025
```

## Detailed Usage

### DateTime Extensions

The library extends `DateTime` with numerous helpful methods:

```csharp
var date = new DateTime(2025, 3, 15);

// Formatting and conversion
date.ToFormattedString(); // "March 15, 2025"
date.ToUtc("Eastern Standard Time"); // Convert to UTC
"2025-03-15".ToDateOrNull(); // Safe string parsing

// Business day operations
date.GetFirstBusinessDayOfMonth(); // First weekday of March
date.GetLastBusinessDayOfMonth(); // Last weekday of March  
date.GetNextBusinessDay(); // Next business day after current date
date.GetBusinessDays(new DateTime(2025, 3, 20)); // Count business days in range

// Date calculations
date.GetFirstDayOfMonth(); // March 1, 2025
date.GetLastDayOfMonth(); // March 31, 2025
date.GetQuarter(); // 1 (Q1)
date.GetWeekOfYear(); // Week number in year
date.IsWeekend(); // false (it's a Saturday)
date.ToMidnight(); // 11:59:59 PM of the same day
```

### Working with Date Ranges

```csharp
// Create date ranges
var range = new DateRange(new DateTime(2025, 1, 1), new DateTime(2025, 1, 31));

// Range properties
Console.WriteLine(range.DaysInBetween); // 30
Console.WriteLine(range.FirstBusinessDay); // First business day in range
Console.WriteLine(range.LastBusinessDay); // Last business day in range

// Range operations
range.Contains(new DateTime(2025, 1, 15)); // true
range.ToDateRangeString(); // "1/1/2025 - 1/31/2025"

// Enumerate dates
foreach (var day in range)
{
    Console.WriteLine(day.ToShortDateString());
}

// Working with DateOnly (no time component)
var dateOnlyRange = new DateOnlyRange(new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 31));

// Open ranges (no end date)
var openRange = new OpenDateRange(DateTime.Today); // From today onwards
```

### Time Periods

```csharp
// Year-Month operations
var yearMonth = new YearMonth(3, 2025); // March 2025
var nextMonth = yearMonth.Next(); // April 2025
var previousMonth = yearMonth.Previous(); // February 2025
var specificDay = yearMonth.Day(15); // March 15, 2025

// Quarterly operations
var quarter = new YearQuarter(1, 2025); // Q1 2025
Console.WriteLine(quarter.Months); // [1, 2, 3] (Jan, Feb, Mar)
var nextQuarter = quarter.Next(); // Q2 2025

// Weekly operations (Standard and ISO)
var week = new YearWeek(10, 2025); // Week 10 of 2025
var isoWeek = new YearWeekIso(10, 2025); // ISO week 10 of 2025

// Year operations
var year = new YearEntity(2025);
Console.WriteLine(year.Months.Count); // 12
Console.WriteLine(year.Quarters.Count); // 4
Console.WriteLine(year.Weeks.Count); // ~52-53

// Current periods
var currentYtd = YearToDate.Current; // Jan 1 to today
var currentMtd = new MonthToDate(); // Month start to today
var ttm = TrailingTwelveMonths.Current; // Last 12 months
```

### Data Grouping and Analysis

```csharp
// Sample data with year-month information
var data = new List<SalesRecord>
{
    new() { Year = 2025, Month = 1, Amount = 1000 },
    new() { Year = 2025, Month = 1, Amount = 1500 },
    new() { Year = 2025, Month = 2, Amount = 2000 }
};

// Group by month
var monthGrouper = new YearMonthGrouper<SalesRecord>(data);
var monthlyGroups = monthGrouper.GroupByDateRange();
var monthlySummary = monthGrouper.GroupByLabel(x => x.Amount);

// Group by quarter  
var quarterGrouper = new YearQuarterGrouper<SalesRecord>(data);
var quarterlyGroups = quarterGrouper.GroupByDateRange();

public record SalesRecord : IYearMonth
{
    public int Year { get; init; }
    public int Month { get; init; }
    public decimal Amount { get; init; }
}
```

### Parsing and Conversion

```csharp
// Parse date ranges
DateRange.TryParse("2025-01-01 - 2025-01-31", out var range);

// Parse periods
YearMonth.TryParse("2025-03", out var yearMonth);
YearQuarter.TryParse("2025-Q2", out var quarter);
YearWeek.TryParse("2025-W10", out var week);

// Convert to records
var monthRecord = yearMonth.ToRecord(); // YearMonthRecord
var quarterRecord = quarter.ToRecord(); // YearQuarterRecord
```

## Interface System

The library provides a comprehensive interface system for consistent API design:

- `IReadOnlyDateRange` - Read-only date range access
- `IYearMonth`, `IYearQuarter`, `IYearWeek` - Period identification
- `IHasBusinessDays` - Business day calculations
- `ILinkedEntity<T>` - Navigation between periods (Previous/Next)
- `IGrouper<T, TConcrete>` - Data grouping capabilities

## Business Applications

This library is particularly useful for:

- **Financial Reporting**: Quarter-over-quarter analysis, year-to-date calculations
- **Business Analytics**: Trailing twelve months analysis, period-over-period comparisons
- **Payroll Systems**: Business day calculations, working day ranges
- **Project Management**: Timeline calculations, business day scheduling
- **Data Analysis**: Grouping time-series data by various periods

## Requirements

- .NET 9.0 or later
- C# 12.0 language features

## Contributing

We welcome contributions! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## About Unosquare

[Unosquare](https://unosquare.com) is a full-service, one-stop shop technology consulting company. We specialize in building and maintaining custom software solutions.
