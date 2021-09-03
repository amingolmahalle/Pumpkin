using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Transaction;
using Pumpkin.Core.Transaction;
using Pumpkin.Data;
using Pumpkin.Web.Filters.Transaction;
using Pumpkin.Web.Filters.Validator;
using Pumpkin.Web.Hosting;

namespace Pumpkin.Web.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseContext<TDbContext>(
            this IServiceCollection services,
            string connectionString,
            Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
            where TDbContext : DatabaseContext
        {
            services.AddDbContext<TDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction)
                    .EnableDetailedErrors();
            });

            services.AddScoped<ITransactionService, TransactionService<TDbContext>>();
        }

        public static void AddMinimalMvc(this IServiceCollection services)
        {
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
                });
        }

        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
    }
}