using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Transaction;
using Pumpkin.Core.Transaction;
using Pumpkin.Data;

namespace Pumpkin.Core.Registration
{
    public static class ServiceCollection
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

        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
    }
}