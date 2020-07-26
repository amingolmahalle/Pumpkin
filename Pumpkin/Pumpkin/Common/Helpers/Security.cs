using System;
using System.Security.Cryptography;
using System.Text;

namespace Pumpkin.Common.Helpers
{
    public static partial class Helpers
    {
        public static string GetSha256Hash(string input)
        {
            using var sha256 = SHA256.Create();
            var byteValue = Encoding.UTF8.GetBytes(input);
            var byteHash = sha256.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}