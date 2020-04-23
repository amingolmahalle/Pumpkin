using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Registration;

namespace Pumpkin.Core.Registration
{
    public static class DynamicallyInstaller
    {
        private static IEnumerable<Type> AllTypes
        {
            get { return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()); }
        }

        public static void NeedToInstallConfig(this IServiceCollection services)
        {
            var typesToRegister = AllTypes
                .Where(it => !(it.IsAbstract || it.IsInterface)
                             && typeof(INeedToInstall).IsAssignableFrom(it));

            foreach (var item in typesToRegister)
            {
                var service = (INeedToInstall) Activator.CreateInstance(item);

                service.Install(services);
            }
        }

        public static void NeedToRegisterMappingConfig(this ModelBuilder modelBuilder)
        {
            var typesToRegister = AllTypes
                .Where(it =>
                    !(it.IsAbstract || it.IsInterface) &&
                    it.GetInterfaces().Any(x =>
                        x.IsGenericType &&
                        x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var item in typesToRegister)
            {
                dynamic service = Activator.CreateInstance(item);

                modelBuilder.ApplyConfiguration(service);
            }
        }

        public static void NeedToRegisterEntitiesConfig(this ModelBuilder modelBuilder)
        {
            var typesToRegister = AllTypes
                .Where(it =>
                    !(it.IsAbstract || it.IsInterface) &&
                    it.GetInterfaces().Any(x =>
                        x.IsGenericType &&
                        x.GetGenericTypeDefinition() == typeof(IEntity)))
                .ToList();

            foreach (var item in typesToRegister)
            {
                modelBuilder.Entity(item);
            }
        }

        public static void NeedToRegisterRepositoriesConfig(this IServiceCollection services)
        {
            // TODO: Dynamically Inject repositories
            // System.Reflection.Assembly.GetExecutingAssembly()
            //     .GetTypes()
            //     .Where(item => item.GetInterfaces()
            //         .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(IRepository<,,>)) && !item.IsAbstract && !item.IsInterface)
            //     .ToList()
            //     .ForEach(assignedTypes =>
            //     {
            //         var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IRepository<,,>));
            //         services.AddScoped(serviceType, assignedTypes);
            //     });
        }
    }
}