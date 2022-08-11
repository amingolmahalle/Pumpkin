using System.Security.Cryptography;
using System.Text;

namespace Pumpkin.Domain.Framework.Helpers;

public static class Crypto
{
    private const int Size = 8;

    public static HashSalt GenerateSaltedHash(string password)
    {
        var saltBytes = new byte[Size];
        var provider = RandomNumberGenerator.Create();
        provider.GetNonZeroBytes(saltBytes);
        var salt = Convert.ToBase64String(saltBytes);
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
        var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(32));

        return new HashSalt {Password = password, Hash = hashPassword, Salt = salt};
    }

    public static HashSalt GenerateHash(string salt, string password)
    {
        var provider = RandomNumberGenerator.Create();
        var saltBytes = Convert.FromBase64String(salt);
        provider.GetNonZeroBytes(saltBytes);
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
        var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(32));

        return new HashSalt {Password = password, Hash = hashPassword, Salt = salt};
    }

    public static string HashPassword(string password) {
        return password.GetSha256HashString();
    }

    private static string GetSha256HashString(this string inputString)
    {
        var sb = new StringBuilder();
        foreach (var b in GetSha256Hash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
    private static IEnumerable<byte> GetSha256Hash(string inputString)
    {
        HashAlgorithm algorithm = SHA256.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }
    public class HashSalt
    {
        public string Password { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}