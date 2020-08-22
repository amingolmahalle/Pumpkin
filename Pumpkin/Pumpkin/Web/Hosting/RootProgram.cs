using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Pumpkin.Web.Hosting
{
    public abstract class RootProgram<TStartup> where TStartup : class
    {
        protected static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).ConfigureLogging(options => options.ClearProviders())
                .UseNLog()
                .UseStartup<TStartup>();
        }
    }
}