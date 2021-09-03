using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Pumpkin.Web.Hosting;

namespace Sample.Test
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureLogging(options => options.ClearProviders())
                .UseNLog();
    }
}