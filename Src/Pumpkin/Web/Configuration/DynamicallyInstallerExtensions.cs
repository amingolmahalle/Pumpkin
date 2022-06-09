using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Caching;
using Pumpkin.Contract.Domain;

namespace Pumpkin.Web.Configuration;

public static class DynamicallyInstallerExtensions
{
    public static void NeedToInstallConfig(this IServiceCollection services)
    {
        var typesToRegister = Common.AssemblyScanner.AllTypes()
            .Where(it => !(it.IsAbstract || it.IsInterface)
                         && typeof(INeedToInstall).IsAssignableFrom(it));

        foreach (var item in typesToRegister)
        {
            var service = (INeedToInstall) Activator.CreateInstance(item);

            service?.Install(services);
        }
    }

    public static void NeedToRegisterCacheProviderConfig(this IServiceCollection services)
    {
        var typesToRegister = Common.AssemblyScanner.AllTypes()
            .Where(it => typeof(ICacheProvider).IsAssignableFrom(it) && !it.IsInterface && !it.IsAbstract)
            .ToList();

        foreach (var item in typesToRegister)
        {
            services.AddScoped(typeof(ICacheProvider), item);
        }
    }
}