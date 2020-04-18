using System;
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
        public static void AddDbContextEx<TDbContext>(
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
    }
}