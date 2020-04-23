using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Caching;
using Pumpkin.Contract.Registration;
using Pumpkin.Contract.Serialization;
using Pumpkin.Core.Caching;
using Pumpkin.Core.Caching.Providers.Shared.Redis;
using Pumpkin.Core.Serialization;

namespace Pumpkin.Core
{
    public class CoreRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            // redis
            services.AddSingleton(typeof(RedisConnectionFactory));
            services.AddScoped(typeof(ICacheService), typeof(CacheService));
            // ---
            
            services.AddTransient<ISerializer, NewtonSoftSerializer>();
        }
    }
}