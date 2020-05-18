using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pumpkin.Contract.Logging;
using Pumpkin.Core.Logging.NLog;
using Pumpkin.Core.Registration;
using Pumpkin.Utils;
using Pumpkin.Web.Filters.Transaction;
using Pumpkin.Web.Filters.Validator;
using Pumpkin.Web.RequestWrapper;
using Pumpkin.Web.Swagger;

namespace Pumpkin.Web.Hosting
{
    public class RootStartup
    {
        protected virtual IEnumerable<int> Versions => new[] {1};

        public virtual void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddCors();

            services.AddHttpContextAccessor();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(options =>
            {
                Versions.ToList()
                    .ForEach(v =>
                        options.SwaggerDoc($"v{v}",
                            new OpenApiInfo
                            {
                                Title = $"{Constants.HostTitle}:v{v}", Version = $"v{v}"
                            }));

                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
            });
            
            NLogConfigurationManager.Configure();
            LogManager.Use<NLogFactory>();

            services.NeedToInstallConfig();
            
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

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                Versions.ToList()
                    .ForEach(v => options.SwaggerEndpoint($"/swagger/v{v}/swagger.json",
                        $"{Constants.HostTitle}:v{v}"));

                options.RoutePrefix = $"{Constants.HostApiRouteDiscriminator}swagger";
            });

            app.UseRouting();

            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}