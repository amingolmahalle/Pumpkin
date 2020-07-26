using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Caching;
using Pumpkin.Contract.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pumpkin.Contract.Transaction;
using Pumpkin.Core.Transaction;
using Pumpkin.Data;

namespace Pumpkin.Core.Registration
{
    public static class DynamicallyInstaller
    {
        public static void NeedToInstallConfig(this IServiceCollection services)
        {
            var typesToRegister = Common.AssemblyScanner.AllTypes
                .Where(it => !(it.IsAbstract || it.IsInterface)
                             && typeof(INeedToInstall).IsAssignableFrom(it));

            foreach (var item in typesToRegister)
            {
                var service = (INeedToInstall) Activator.CreateInstance(item);

                service.Install(services);
            }
        }

        public static void NeedToRegisterCacheProviderConfig(this IServiceCollection services)
        {
            var typesToRegister = Common.AssemblyScanner.AllTypes
                .Where(it => typeof(ICacheProvider).IsAssignableFrom(it) && !it.IsInterface && !it.IsAbstract)
                .ToList();

            foreach (var item in typesToRegister)
            {
                services.AddScoped(typeof(ICacheProvider), item);
            }
        }
        
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
    }
}