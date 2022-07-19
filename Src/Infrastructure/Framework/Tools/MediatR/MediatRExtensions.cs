using System.Reflection;
using Framework.Exceptions;
using Framework.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Framework.Tools.MediatR;

public static class MediatRExtensions
{
    /// <summary>
    /// Registers handlers and mediator types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="assemblies">Assemblies to scan</param>        
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services, params Assembly[] assemblies)
        => services.AddMediatR(assemblies, configuration: null);
    
    /// <summary>
    /// Registers handlers and mediator types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="assemblies">Assemblies to scan</param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services,
        Action<MediatRServiceConfiguration> configuration, params Assembly[] assemblies)
        => services.AddMediatR(assemblies, configuration);
    
    /// <summary>
    /// Registers handlers and mediator types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="assemblies">Assemblies to scan</param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services, IEnumerable<Assembly> assemblies, Action<MediatRServiceConfiguration> configuration)
    {
        var all = assemblies?.ToList() ?? new List<Assembly>();
        if (!all.Any())
            throw new Dexception(Situation.Make(SitKeys.Forbidden),
                new List<KeyValuePair<string, string>> {new(":پیام:", "امکان آماده‌سازی سیستم وجود ندارد.")});

        var serviceConfig = new MediatRServiceConfiguration();
        configuration?.Invoke(serviceConfig);
        MediatRServiceRegistrar.AddRequiredServices(services, serviceConfig);
        MediatRServiceRegistrar.AddMediatRClasses(services, all);

        return services;
    }

    /// <summary>
    /// Registers handlers and mediator types from the assemblies that contain the specified types
    /// </summary>
    /// <param name="services"></param>
    /// <param name="handlerAssemblyMarkerTypes"></param>        
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
        => services.AddMediatR(handlerAssemblyMarkerTypes, configuration: null);

    public static IServiceCollection AddMediatR(this IServiceCollection services, string nameSpace)
    {
        var handlerAssemblyMarkerTypes = AssemblyScanner.AllTypes(nameSpace, "(.*)")
            .Where(it => !(it.IsAbstract || it.IsInterface));
        return services.AddMediatR(handlerAssemblyMarkerTypes, configuration: null);
    }

    /// <summary>
    /// Registers handlers and mediator types from the assemblies that contain the specified types
    /// </summary>
    /// <param name="services"></param>
    /// <param name="handlerAssemblyMarkerTypes"></param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services,
        Action<MediatRServiceConfiguration> configuration, params Type[] handlerAssemblyMarkerTypes)
        => services.AddMediatR(handlerAssemblyMarkerTypes, configuration);
    
    /// <summary>
    /// Registers handlers and mediator types from the assemblies that contain the specified types
    /// </summary>
    /// <param name="services"></param>
    /// <param name="handlerAssemblyMarkerTypes"></param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services,
        IEnumerable<Type> handlerAssemblyMarkerTypes,
        Action<MediatRServiceConfiguration> configuration)
        => services.AddMediatR(handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly), configuration);
}