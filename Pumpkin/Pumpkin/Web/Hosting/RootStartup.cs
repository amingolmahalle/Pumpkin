using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pumpkin.Core;
using Pumpkin.Web.RequestWrapper;

namespace Pumpkin.Web.Hosting
{
    public class RootStartup
    {
        private readonly IConfiguration _configuration;


        public RootStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private List<string> GetRequestInterceptorExceptions()
        {
            return new List<string>();
        }
        
        public virtual void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var builder = services.AddMvcCore().AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            builder.AddCors();

            services.NeedToInstallConfig();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();

            app.UseRequestInterceptor(GetRequestInterceptorExceptions().Union(new List<string>(){"/swagger/"}).ToList());
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}