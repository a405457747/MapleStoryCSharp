using System;

public  class TimeHelper
{
    private const double Second = 1;
    private const double Minute = Second * 60;
    private const double Hour = Minute * 60;
    public const double Day = Hour * 24;
    public const string DateFormat = "yyyy_MMdd_HHmm";

    public static double GetNowGreenwichTimeStamp(TimeStampTypes timeStampTypes = TimeStampTypes.Second)
    {
        double res = 0f;
        var ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
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
}

public enum TimeStampTypes
{
    Second,
    Minute,
    Hour,
    Day
}