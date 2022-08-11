using Microsoft.Extensions.Configuration;

namespace Pumpkin.Domain.Framework.Helpers;

public static class GlobalConfig
{
    public static IConfiguration Config { get; set; }
}