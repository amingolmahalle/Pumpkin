using Microsoft.Extensions.Configuration;

namespace Pumpkin.Web.Configs
{
    public static class ConfigManager
    {
        public static string GetConnectionString(string connectionName)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            return configuration.GetConnectionString(connectionName);
        }
    }
}