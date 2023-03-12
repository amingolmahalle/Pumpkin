using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Framework.Helpers;

namespace Pumpkin.Infrastructure.Framework.Extensions;

public static class ModelBuilderExtensions
{
    public static void NeedToRegisterMappingConfig(this ModelBuilder modelBuilder)
    {
        var typesToRegister = AssemblyScanner.AllTypes("Pumpkin.Infrastructure", "(.*)")
            .Where(it =>
                !(it.IsAbstract || it.IsInterface)
                && it.GetInterfaces().Any(x =>
                    x.IsGenericType
                    && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            .ToList();

        foreach (var item in typesToRegister)
        {
            dynamic service = Activator.CreateInstance(item);

            modelBuilder.ApplyConfiguration(service);
        }
    }

    public static void NeedToRegisterEntitiesConfig<T>(this ModelBuilder modelBuilder)
    {
        var typesToRegister = AssemblyScanner.AllTypes("Pumpkin.Domain", "(.*)")
            .Where(it =>
                !(it.IsAbstract || it.IsInterface)
                && it.GetInterfaces().Any(x => x == typeof(T)))
            .ToList();

        foreach (var item in typesToRegister)
            modelBuilder.Entity(item);
    }
}