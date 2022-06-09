using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Caching;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Events;
using Pumpkin.Contract.Serialization;
using Pumpkin.Core.Caching;
using Pumpkin.Core.Caching.Providers.Shared.Redis;
using Pumpkin.Core.Events;
using Pumpkin.Core.Serialization;
using Pumpkin.Web.Configuration;

namespace Pumpkin.Core;

public class CoreRegistrator : INeedToInstall
{
    public void Install(IServiceCollection services)
    {
        // Redis
        services.AddSingleton(typeof(RedisConnectionFactory));
        services.NeedToRegisterCacheProviderConfig();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IMemoryCache, MemoryCache>();
        
        //Rabbitmq
        services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
        services.AddSingleton<BusHandler, RabbitMqBusHandler>();

        //NewtonSoft
        services.AddTransient<ISerializer, NewtonSoftSerializer>();
    }
}