
namespace ClientWebApi;

public class Program
{
    public static bool SetupConsumers;

    public static async Task Main(string[] args)
        => await CreateHostBuilder(args).Build().RunAsync();

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();

                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                    .AddCommandLine(args)
                    .Build();

                var applicationUrls = (Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://*:7020;https://*:7025")
                    .Split(';', StringSplitOptions.RemoveEmptyEntries).ToArray();

                webBuilder.UseUrls(applicationUrls);
            });
}