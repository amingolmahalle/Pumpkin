using Microsoft.Extensions.Hosting;
using NLog;

namespace Pumpkin.Web.Hosting;

public static class ProgramExtension
{
    public static void Run(this IHostBuilder createHostBuilder)
    {
        var logger = LogManager.GetCurrentClassLogger();

        try
        {
            createHostBuilder.Build().Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Stopped Program Because of exception");
            throw;
        }
        finally
        {
            LogManager.Flush();
            LogManager.Shutdown();
        }
    }
}