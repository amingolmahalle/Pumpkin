using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Pumpkin.Common.Extensions
{
    public static partial class Extensions
    {
        public static string En2Fa(this string str)
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

        public static string Fa2En(this string str)
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

        public static string ToFormalPhoneNumber(this string input)
        {
            input = input?.Trim()?.Fa2En();

            if (!IsMobileNumber(input))
            {
                return string.Empty;
            }

            if (input != null && input[0] != '0')
            {
                if (input[0] == '+')
                {
                    input = input.TrimStart('+');
                }

                if (input[0] == '9' && input[1] == '8' && input[2] == '9') //input.StartsWith("989")
                {
                    int len = input.Length - 3;
                    input = "0" + input.Substring(2, len + 1);
                }

                if (input[0] != '0')
                {
                    input = "0" + input;
                }
            }

            return input;
        }


        public static bool IsMobileNumber(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, Constants.MobileNumberPattern);
        }


        public static long ToFormalPhoneNumberWithoutZero(this string input)
        {
            string formal = ToFormalPhoneNumber(input);

            if (string.IsNullOrWhiteSpace(formal))
            {
                // throw new FormalPhoneGenerationException();
            }

            return System.Convert.ToInt64(formal?.Substring(1, formal.Length - 1));
        }

        public static string ToFormalEmail(this string input)
        {
            var formal = input.Trim();

            if (string.IsNullOrWhiteSpace(formal) || !formal.IsValidEmail())
            {
                // throw new FormalEmailGenerationException();
            }

            return formal;
        }

        public static bool IsValidEmail(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            try
            {
                return Regex.IsMatch(input, Constants.EmailPattern,
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch
            {
                return false;
            }
        }


        public static string NullIfEmpty(this string str)
        {
            return str?.Length == 0 ? null : str;
        }

        public static T ToEnum<T>(this string value)
        {
            Enum.TryParse(typeof(T), value, true, out var res);

            return (T) res;
        }

        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        public static bool IsValidJson(this string text)
        {
            text = text.Trim();
            if ((text.StartsWith("{") && text.EndsWith("}")) || // For object
                (text.StartsWith("[") && text.EndsWith("]"))) // For array
            {
                try
                {
                    // var obj = JToken.Parse(text);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }
    }
}