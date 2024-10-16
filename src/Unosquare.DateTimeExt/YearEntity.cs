﻿namespace Unosquare.DateTimeExt;

public sealed class YearEntity(int? year = null) : YearAbstract(new(year ?? DateTime.UtcNow.Year, 1, 1),
    new(year ?? DateTime.UtcNow.Year, 12, 31))
{
    public YearEntity(DateTime? dateTime)
        : this((dateTime ?? DateTime.UtcNow).Year)
    {
    }

    public static YearEntity Current => new();

    public YearEntity Next => new(StartDate.AddYears(1));

    public YearEntity Previous => new(StartDate.AddYears(-1));
}