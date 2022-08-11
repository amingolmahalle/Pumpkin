using Microsoft.Extensions.Configuration;

namespace Domain.Framework.Helpers;

public static class GlobalConfig
{
    public static IConfiguration Config { get; set; }
}