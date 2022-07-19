using Microsoft.AspNetCore.Http;

namespace Domain.Framework.Extensions;

public static partial class Extensions
{
    public static string AuthToken(this HttpRequest request)
        => ReadHeader(request, "Authorization");

    public static string DeviceId(this HttpRequest request)
        => ReadHeader(request, "DeviceId");

    public static string ClientIp(this HttpRequest request)
    {
        var ip = ReadHeader(request, "XX-FORWARDED-FOR");
        if (!string.IsNullOrEmpty(ip))
            return ip;

        ip = ReadHeader(request, "X-FORWARDED-FOR");
        if (!string.IsNullOrEmpty(ip))
            return ip;

        ip = ReadHeader(request, "X-REAL-IP");
        if (!string.IsNullOrEmpty(ip))
            return ip;

        ip = ReadHeader(request, "REMOTE_ADDR");
        return !string.IsNullOrEmpty(ip) ? ip : null;
    }

    public static string GetHeader(this HttpRequest request, string key)
        => ReadHeader(request, key);
        
    private static string ReadHeader(HttpRequest request, string key)
    {
        if (!(request?.Headers.ContainsKey(key) ?? false))
            return default;

        var value = request.Headers[key].ToArray().FirstOrDefault();
        
        return string.IsNullOrWhiteSpace(value) ? default : value;
    }
}