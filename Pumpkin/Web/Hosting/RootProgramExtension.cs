using System;
using Microsoft.AspNetCore.Hosting;
using NLog;

namespace Pumpkin.Web.Hosting
{
    public static class RootProgramExtension
    {
        public static void Run(this IWebHostBuilder createWebHostBuilder)
        {
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                createWebHostBuilder.Build().Run();
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
}