using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Logging;
using Pumpkin.Core.Logging.NLog;
using Pumpkin.Web.Configuration;
using Pumpkin.Web.RequestWrapper;

namespace Pumpkin.Web.Hosting
{
    public class RootStartup
    {
        protected readonly IConfiguration Configuration;

        public RootStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddCors();

            services.AddHttpContextAccessor();

            services.AddCustomApiVersioning();

            NLogConfigurationManager.Configure();
            LogManager.Use<NLogFactory>();

            services.NeedToInstallConfig();

            services.AddMinimalMvc();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomCors();

            app.UseRequestInterceptor();

            app.UseHsts(env);

            ConfigureBeforeMvc(app);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public virtual void ConfigureBeforeMvc(IApplicationBuilder app)
        {
        }
    }
}