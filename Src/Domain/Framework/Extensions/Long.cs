namespace Domain.Framework.Extensions;

public static partial class Extensions
{
    public static DateTime ToLocalDateTime(this long milliseconds)
    {
        var date = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
        date = DateTime.SpecifyKind(date, DateTimeKind.Local);
        
        return date;
    }
    
    public static DateTime ToUtcDateTime(this long milliseconds)
    {
        var date = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        
        return date;
    }
    
    public static DateTime ToLocalDateTimeFromSeconds(this long seconds)
    {
        var date = DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;
        date = DateTime.SpecifyKind(date, DateTimeKind.Local);
        
        return date;
    }
    
    public static DateTime ToUtcDateTimeFromSeconds(this long seconds)
    {
        var date = DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        
        return date;
    }
}