using System;

public class TimeHelper
{
    public const int Second = 1;
    public const int Minute = Second * 60;
    public const int Hour = Minute * 60;
    public const int Day = Hour * 24;
    public const int Week = Day * 7;
    public const int Month = Day * 30;
    public const string DateFormat = "yyyy_MMdd_HHmm";
    public static readonly DateTime Greenwich = CreateDateTime(1970, 1, 1);

    public static double GetNowTimeStamp(TimeStampTypes timeStampTypes = TimeStampTypes.Second,
        DateTime algo = default)
    {
        if (algo == default) algo = Greenwich;

        double res = 0f;
        var ts = DateTime.Now - algo;
        switch (timeStampTypes)
        {
            case TimeStampTypes.Second:
                res = ts.TotalSeconds;
                break;
            case TimeStampTypes.Minute:
                res = ts.TotalMinutes;
                break;
            case TimeStampTypes.Hour:
                res = ts.TotalHours;
                break;
            case TimeStampTypes.Day:
                res = ts.TotalDays;
                break;
        }

        return res;
    }

    public static DateTime CreateDateTime(int year = 0, int month = 0, int day = 0, int hour = 0, int minute = 0,
        int second = 0)
    {
        return new DateTime(year, month, day, hour, minute, second);
    }

    public static string GetTimeSpanFormat(int f)
    {
        var d = new TimeSpan(0, 0, f);
        return d.ToString();
    }

    public static bool IsBelongTimeQuantum(int lMonth, int lDay, int rMontn, int rDay)
    {
        var now = DateTime.Now;
        var year = now.Year;
        var l = CreateDateTime(year, lMonth, lDay);
        var r = CreateDateTime(year, rMontn, rDay);
        return now > l && now < r;
    }
}

public enum TimeStampTypes
{
    Second,
    Minute,
    Hour,
    Day
}