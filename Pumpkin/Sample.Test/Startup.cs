using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Core.Registration;
using Pumpkin.Web.Configs;
using Pumpkin.Web.Hosting;
using Sample.Test.Data;

namespace Sample.Test
{
    public class Startup : RootStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddDatabaseContext<ApplicationDbContext>(ConfigManager.GetConnectionString("SqlServer"));
        }
    }
}