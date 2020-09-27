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
        //private readonly SecuritySettings _securitySettings;

        protected readonly IConfiguration Configuration;

        public RootStartup(IConfiguration configuration) //, SecuritySettings securitySettings)
        {
            Configuration = configuration;
            // _securitySettings = securitySettings;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //   services.Configure<SecuritySettings>(Configuration.GetSection(nameof(SecuritySettings)));

            services.AddCors();

            services.AddHttpContextAccessor();

            // services.AddJwtAuthentication(_securitySettings.JwtSettings);

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

            UseBeforeMvc(app);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public virtual void UseBeforeMvc(IApplicationBuilder app)
        {
        }
    }
}