using System.Globalization;
using System.Text.RegularExpressions;

namespace Pumpkin.Domain.Framework.Extensions;

public static partial class Extensions
{
    public static DateTime ToDateTime(this string persianDate)
    {
        if (string.IsNullOrEmpty(persianDate))
            return DateTime.MinValue;

        var inputParts = persianDate.Split('T');
        if (inputParts.Length == 0)
            return DateTime.MinValue;

        var dateParts = inputParts[0].Split('/');
        if (dateParts.Length != 3)
            return DateTime.MinValue;

        var timeParts = inputParts.Length > 1 ? inputParts[1].Split('/') : new[] {"0", "0", "0"};
        if (timeParts.Length != 3)
            return DateTime.MinValue;

        var dateTime = new DateTime(int.Parse(dateParts[0]), int.Parse(dateParts[1]), int.Parse(dateParts[2]),
            int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]), new PersianCalendar());
        return DateTime.Parse(dateTime.ToString(CultureInfo.CreateSpecificCulture("en-US")));
    }

    public static string ToFaNumeric(this string str)
    {
        return str.Replace("0", "۰")
            .Replace("1", "۱")
            .Replace("2", "۲")
            .Replace("3", "۳")
            .Replace("4", "۴")
            .Replace("5", "۵")
            .Replace("6", "۶")
            .Replace("7", "۷")
            .Replace("8", "۸")
            .Replace("9", "۹");
    }

    public static string ToEnNumeric(this string str)
    {
        return str.Replace("۰", "0")
            .Replace("۱", "1")
            .Replace("۲", "2")
            .Replace("۳", "3")
            .Replace("۴", "4")
            .Replace("۵", "5")
            .Replace("۶", "6")
            .Replace("۷", "7")
            .Replace("۸", "8")
            .Replace("۹", "9")
            //iphone numeric
            .Replace("٠", "0")
            .Replace("١", "1")
            .Replace("٢", "2")
            .Replace("٣", "3")
            .Replace("٤", "4")
            .Replace("٥", "5")
            .Replace("٦", "6")
            .Replace("٧", "7")
            .Replace("٨", "8")
            .Replace("٩", "9");
    }

    public static string FixPersianChars(this string str)
    {
        return str.Replace("ﮎ", "ک")
            .Replace("ﮏ", "ک")
            .Replace("ﮐ", "ک")
            .Replace("ﮑ", "ک")
            .Replace("ك", "ک")
            .Replace("ي", "ی")
            .Replace(" ", " ")
            .Replace("‌", " ")
            .Replace("ھ", "ه"); //.Replace("ئ", "ی");
    }

    public static string NullIfEmpty(this string str)
    {
        return str?.Length == 0 ? null : str;
    }

    public static T ToEnum<T>(this string value)
    {
        Enum.TryParse(typeof(T), value, true, out var res);

        return (T) res!;
    }

    public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
    {
        return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
    }

    public static bool IsValidEmail(this string strIn)
    {
        if (string.IsNullOrWhiteSpace(strIn))
            return false;
        try
        {
            return Regex.IsMatch(strIn,
                @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidPhoneNumber(this string input)
    {
        return !string.IsNullOrWhiteSpace(input) &&
               Regex.IsMatch(input, @"(^09\d{9}$)|(^\+989\d{9}$)|(^9\d{9}$)|(^989\d{9}$)");
    }

    public static string UrlCase(this string input)
    {
        return Regex.Replace(input, "([a-z])([A-Z])", "$1-$2").ToLower();
    }

    public static bool IsValidNationalCode(this string input)
    {
        return !string.IsNullOrWhiteSpace(input) &&
               Regex.IsMatch(input, @"^(\d{10})$");
    }
}