using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Helpers;
using Pumpkin.Domain.Framework.Services;

namespace Pumpkin.Infrastructure.Framework.Extensions;

public static class DynamicallyInstallerExtensions
{
    public static void DynamicInject(this IServiceCollection services, IConfiguration configuration, string nameSpace)
    {
        var typesToRegister = AssemblyScanner.AllTypes(nameSpace, "(Infrastructure)+")
            .Where(it => !(it.IsAbstract || it.IsInterface) && typeof(IHaveInjection).IsAssignableFrom(it))
            .ToList();
        typesToRegister.AddRange(AssemblyScanner.AllTypes(nameSpace, "(Application)+")
            .Where(it => !(it.IsAbstract || it.IsInterface) && typeof(IHaveInjection).IsAssignableFrom(it)));

        foreach (var item in typesToRegister)
        {
            var service = (IHaveInjection) Activator.CreateInstance(item);
            try
            {
                service?.Inject(services, configuration);
            }
            catch // (Exception exception)
            {
                // exception.Log();
            }
        }
    }
}