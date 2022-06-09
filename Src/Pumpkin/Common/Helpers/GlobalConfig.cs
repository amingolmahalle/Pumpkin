using Microsoft.Extensions.Configuration;

namespace Pumpkin.Common.Helpers;

public static class GlobalConfig
{
    public static IConfiguration Config { get; set; }
}