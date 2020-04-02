using System;
using System.Text.RegularExpressions;

namespace Sample.Test.Helper
{
    public  static class MobileNumberExtension
    {
        private const string MobileNumberPattern = @"^((\+|00)?98|0)?(?<number>9\d{9})$";

        private static readonly Regex MobileNumberRegex = new Regex(MobileNumberPattern, RegexOptions.Compiled);

        public static string MobileNumberNormalize(this string mobileNumber)
        {
            var match = MobileNumberRegex.Match(mobileNumber);

            if (!match.Success)
                throw new MobileNumberIsNotValidException(mobileNumber);

            return match.Groups["number"].Value;
        }
    }

    public class MobileNumberIsNotValidException : ArgumentException
    {
        public MobileNumberIsNotValidException(string invalidMobileNumber) : base(
            $"mobile number '{invalidMobileNumber}' is not valid.")
        {
        }
    }
}