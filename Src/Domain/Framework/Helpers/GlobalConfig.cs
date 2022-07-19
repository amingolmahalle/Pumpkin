using Microsoft.Extensions.Configuration;

namespace Framework.Helpers;

public static class GlobalConfig
{
    public static IConfiguration Config { get; set; }
}