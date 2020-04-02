using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sample.Test.Helper
{
    public static class NationalCodeExtension
    {
        private static readonly Regex ForeignNationalCodeRegex = new Regex(@"^\d{11}$", RegexOptions.Compiled);

        public static bool IsValidNationalCode(this string nationalCode)
        {

            if (string.IsNullOrEmpty(nationalCode))
                return false;

            if (nationalCode.Length != 10)
                return false;

            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                return false;

            var allDigitEqual = new[]
            {
                "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666",
                "7777777777", "8888888888", "9999999999"
            };
            if (allDigitEqual.Contains(nationalCode))
                return false;

            var j = 10;
            var sum = 0;
            var array = nationalCode.ToCharArray();
            for (var i = 0; i < array.Length - 1; i++)
            {
                sum += int.Parse(array[i].ToString(CultureInfo.InvariantCulture)) * j;
                j--;
            }

            var div = sum / 11;
            var r = div * 11;
            var diff = Math.Abs(sum - r);
            if (diff <= 2)
            {
                return diff == int.Parse(array[9].ToString(CultureInfo.InvariantCulture));
            }

            var temp = Math.Abs(diff - 11);
            return temp == int.Parse(array[9].ToString(CultureInfo.InvariantCulture));
        }

        public static bool IsValidForeignNationalCode(this string nationalCode)
        {
            return ForeignNationalCodeRegex.IsMatch(nationalCode);
        }
    }
}