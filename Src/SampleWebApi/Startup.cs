using Pumpkin.Web.Configuration;
using Pumpkin.Web.Hosting;
using SampleWebApi.Data;
using SampleWebApi.Web.Configuration;

namespace SampleWebApi
{
    public class Startup : RootStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddCustomSwagger();

            services.AddDatabaseContext<ApplicationDbContext>(ConfigManager.GetConnectionString("SqlServer"));
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.InitializeDatabase<ApplicationDbContext>();

            base.Configure(app, env);
        }

        public override void ConfigureBeforeMvc(IApplicationBuilder app)
        {
            base.ConfigureBeforeMvc(app);
            
            app.UseSwaggerAndUi();
        }
    }
}