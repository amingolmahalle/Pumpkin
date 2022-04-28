namespace Pumpkin.Common.Helpers;

public static partial class Helpers
{
    public static string RandomRange(int min, int max, int range)
    {
        var random = new Random();
        return random.Next(min, max).ToString();
    }
}