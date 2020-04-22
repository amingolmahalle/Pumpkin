using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pumpkin.Core.Registration;
using Pumpkin.Web.Filters.Transaction;
using Pumpkin.Web.Filters.Validator;
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

        public virtual void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddHttpContextAccessor();
            services.AddCors();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            // services.AddControllers(
            //         options =>
            //         {
            //            
            //         }
            //     ).AddFluentValidation(fv =>
            //     {
            //         fv.RegisterValidatorsFromAssemblies(RegisterFluentValidation);
            //         fv.ImplicitlyValidateChildProperties = true;
            //     })
            services.AddControllers(options =>
                {
                    options.Filters.Add<TransactionActionFilter>();
                    options.Filters.Add<ValidatorActionFilter>();
                }).ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                }).AddFluentValidation(fv =>
                {
                fv.RegisterValidatorsFromAssemblyContaining<RootStartup>();
                    fv.ImplicitlyValidateChildProperties = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.NeedToInstallConfig();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRequestInterceptor();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}