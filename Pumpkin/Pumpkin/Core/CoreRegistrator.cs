using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Caching;
using Pumpkin.Contract.Registration;
using Pumpkin.Contract.Serialization;
using Pumpkin.Core.Caching;
using Pumpkin.Core.Caching.Providers.Shared.Redis;
using Pumpkin.Core.Registration;
using Pumpkin.Core.Serialization;

namespace Pumpkin.Core
{
    public class CoreRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            // Redis
            services.AddSingleton(typeof(RedisConnectionFactory));
            services.NeedToRegisterCacheProviderConfig();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IMemoryCache, MemoryCache>();
            // Redis

            services.AddTransient<ISerializer, NewtonSoftSerializer>();
        }
    }
}