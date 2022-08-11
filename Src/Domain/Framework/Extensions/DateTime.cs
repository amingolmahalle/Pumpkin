using System.Globalization;

namespace Pumpkin.Domain.Framework.Extensions;

public static partial class Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime">Gregorian DateTime</param>
    /// <param name="format">
    /// "d" => "yyyy/MM/dd"
    /// "D" => "dddd dd MMMM yyyy"
    /// "s" => "yyyy-MM-ddTHH:mm:ss"
    /// "t" => "hh:mm"
    /// "T" => "hh:mm:ss"
    /// "Y" => "MMMM yyyy"
    /// "f" => "dddd dd MMMM yyyy - hh:mm tt"
    /// "f1" => "dddd dd MMMM yyyy - HH:mm"
    /// "f2" => "dddd dd MMMM yyyy ساعت hh:mm tt"
    /// "f3" => "dddd dd MMMM yyyy ساعت HH:mm"
    /// "f4" => "ساعت hh:mm tt dddd dd MMMM yyyy"
    /// "F" => "dddd dd MMMM yyyy - hh:mm:ss tt"
    /// "F1" => "dddd dd MMMM yyyy - HH:mm:ss"
    /// "F2" => "dddd dd MMMM yyyy ساعت hh:mm:ss tt"
    /// "F3" => "dddd dd MMMM yyyy ساعت HH:mm:ss"
    /// "F4" => "ساعت hh:mm:ss tt dddd dd MMMM yyyy"
    /// "f5" => "ساعت HH:mm:ss dddd dd MMMM yyyy"
    /// </param>
    /// <returns></returns>
    public static string AsPersian(this DateTime dateTime, string format = "d")
    {
        if (dateTime == default)
            return string.Empty;

        var p = new PersianCalendar();
        var year = p.GetYear(dateTime);
        var month = p.GetMonth(dateTime);
        var monthName = month switch
        {
            1 => "فروردین",
            2 => "اردیبهشت",
            3 => "خرداد",
            4 => "تیر",
            5 => "مرداد",
            6 => "شهریور",
            7 => "مهر",
            8 => "آبان",
            9 => "آذر",
            10 => "دی",
            11 => "بهمن",
            12 => "اسفند",
            _ => ""
        };
        var dayOfMonth = p.GetDayOfMonth(dateTime);
        var dayOfWeek = p.GetDayOfWeek(dateTime);
        var dayOfWeekName = dayOfWeek switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یکشنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه‌شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
            DayOfWeek.Thursday => "پنج‌شنبه",
            DayOfWeek.Friday => "جمعه",
            _ => ""
        };
        var dayOfWeekShortName = dayOfWeek switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یکشنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه‌شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
            DayOfWeek.Thursday => "پنج‌شنبه",
            DayOfWeek.Friday => "جمعه",
            _ => ""
        };

        var hour = p.GetHour(dateTime);
        var minute = p.GetMinute(dateTime);
        var second = p.GetSecond(dateTime);

        format = format switch
        {
            "d" => "yyyy/MM/dd",
            "D" => "dddd dd MMMM yyyy",
            "s" => "yyyy-MM-ddTHH:mm:ss",
            "t" => "hh:mm",
            "T" => "hh:mm:ss",
            "Y" => "MMMM yyyy",

            "f" => "dddd dd MMMM yyyy - hh:mm tt",
            "f1" => "dddd dd MMMM yyyy - HH:mm",
            "f2" => "dddd dd MMMM yyyy ساعت hh:mm tt",
            "f3" => "dddd dd MMMM yyyy ساعت HH:mm",
            "f4" => "ساعت hh:mm tt dddd dd MMMM yyyy",

            "F" => "dddd dd MMMM yyyy - hh:mm:ss tt",
            "F1" => "dddd dd MMMM yyyy - HH:mm:ss",
            "F2" => "dddd dd MMMM yyyy ساعت hh:mm:ss tt",
            "F3" => "dddd dd MMMM yyyy ساعت HH:mm:ss",
            "F4" => "ساعت hh:mm:ss tt dddd dd MMMM yyyy",
            "f5" => "ساعت HH:mm:ss dddd dd MMMM yyyy",

            _ => format
        };

        return format
                .Replace("yyyy", year.ToString("D0"))
                .Replace("yyy", year.ToString().Substring(1, 3))
                .Replace("yy", year.ToString().Substring(2, 2))
                .Replace("MMMM", monthName)
                .Replace("MMM", monthName)
                .Replace("MM", month.ToString("D2"))
                .Replace("MM", year.ToString("D0"))
                .Replace("dddd", dayOfWeekName)
                .Replace("ddd", dayOfWeekShortName)
                .Replace("dd", dayOfMonth.ToString("D2"))
                .Replace("d", dayOfMonth.ToString("D0"))
                .Replace("HH", hour.ToString("D2"))
                .Replace("H", hour.ToString("D0"))
                .Replace("hh", (hour > 12 ? hour - 12 : hour).ToString("D2"))
                .Replace("h", (hour > 12 ? hour - 12 : hour).ToString("D0"))
                .Replace("mm", minute.ToString("D2"))
                .Replace("mm", minute.ToString("D0"))
                .Replace("ss", second.ToString("D2"))
                .Replace("s", second.ToString("D0"))
                .Replace("tt", hour < 12 ? "ق.ظ" : "ب.ظ")
                .Replace("t", hour < 12 ? "ق" : "ب")
            ;
    }

    public static long ToUnixMilliseconds(this DateTime dateTimeValue)
        => ((DateTimeOffset) dateTimeValue).ToUnixTimeMilliseconds();

    public static long ToUtcUnixMilliseconds(this DateTime dateTimeValue)
    {
        dateTimeValue = DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Utc);
        return ((DateTimeOffset) dateTimeValue).ToUnixTimeMilliseconds();
    }

    public static long ToUnixSeconds(this DateTime dateTimeValue)
        => ((DateTimeOffset) dateTimeValue).ToUnixTimeSeconds();

    public static long ToUtcUnixSeconds(this DateTime dateTimeValue)
    {
        dateTimeValue = DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Utc);
        return ((DateTimeOffset) dateTimeValue).ToUnixTimeSeconds();
    }
}