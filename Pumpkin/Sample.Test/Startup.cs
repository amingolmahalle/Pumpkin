using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Core.Registration;
using Pumpkin.Web.Configs;
using Pumpkin.Web.Hosting;
using Sample.Test.Data;

namespace Sample.Test
{
    public class Startup : RootStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddDatabaseContext<ApplicationDbContext>(ConfigManager.GetConnectionString("SqlServer"));
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.InitializeDatabase<ApplicationDbContext>();

            base.Configure(app, env);
        }
    }
}