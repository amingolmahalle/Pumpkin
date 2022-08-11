using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Caching;
using Pumpkin.Domain.Framework.Extensions;

namespace Pumpkin.Infrastructure.Framework.Caching;

public class CacheService : ICacheService
{
    private readonly IServiceProvider _serviceProvider;
        
    private static List<ICacheProvider> _allProviders;

    private static ConcurrentDictionary<CacheProviderType, List<ICacheProvider>> _providers;

    public CacheService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        if (_allProviders == null)
        {
            _allProviders = GetAllProviders();
        }

        _providers = new ConcurrentDictionary<CacheProviderType, List<ICacheProvider>>();
    }

    public T Get<T>(string key, string group, CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            var res = provider.Get<T>(key, group);

            if (res != null)
                return res;
        }

        return default;
    }

    public async Task<T> GetAsync<T>(string key, string group, CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            var res = await provider.GetAsync<T>(key, group);

            if (res != null)
                return res;
        }

        return default;
    }

    public T GetOrCreate<T>(string key, string group, DateTime expiry, Func<T> method, CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            var res = provider.GetOrCreate(key, group, expiry, method);

            if (res != null && !res.Equals(default(T)))
                return res;
        }

        return default;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, string group, DateTime expiry, Func<Task<T>> method,
        CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            var res = await provider.GetOrCreateAsync(key, group, expiry, method);

            if (res != null && !res.Equals(default(T)))
                return res;
        }

        return default;
    }

    public IDictionary<string, T> GetMany<T>(IEnumerable<string> keys, string group, CacheOptions options = null)
        where T : class, new()
    {
        var list = new Dictionary<string, T>();
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            var res = provider.GetMany<T>(keys, group);

            if (res.HasItem())
            {
                foreach (var item in res)
                {
                    if (!list.ContainsKey(item.Key) || list[item.Key] == null)
                        list[item.Key] = item.Value;
                }

                var misses = list.Where(p => p.Value == null);

                if (!misses.HasItem())
                    break;
            }
        }

        return list.HasItem() ? list : null;
    }

    public IDictionary<string, object> GetMany(IEnumerable<string> keys, string group, CacheOptions options = null)
    {
        var list = new Dictionary<string, object>();
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            var enumerable = keys as string[] ?? keys.ToArray();
            var res = provider.GetMany(enumerable, group);

            if (res.HasItem())
            {
                foreach (var item in res)
                {
                    if (!list.ContainsKey(item.Key) || list[item.Key] == null)
                        list[item.Key] = item.Value;
                }

                var misses = list.Where(p => p.Value == null);

                if (!misses.HasItem())
                    break;
            }
        }

        return list.HasItem() ? list : null;
    }

    public async Task<IDictionary<string, object>> GetManyAsync(IEnumerable<string> keys, string group,
        CacheOptions options = null)
    {
        var list = new Dictionary<string, object>();
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            var res = await provider.GetManyAsync(keys, group);

            if (res.HasItem())
            {
                foreach (var item in res)
                {
                    if (!list.ContainsKey(item.Key) || list[item.Key] == null)
                        list[item.Key] = item.Value;
                }

                var misses = list.Where(p => p.Value == null);

                if (!misses.HasItem())
                    break;
            }
        }

        return list.HasItem() ? list : null;
    }

    public async Task<IDictionary<string, T>> GetManyAsync<T>(IEnumerable<string> keys, string group,
        CacheOptions options = null) where T : class, new()
    {
        var list = new Dictionary<string, T>();
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            var res = await provider.GetManyAsync<T>(keys, group);

            if (res.HasItem())
            {
                foreach (var item in res)
                {
                    if (!list.ContainsKey(item.Key) || list[item.Key] == null)
                        list[item.Key] = item.Value;
                }

                var misses = list.Where(p => p.Value == null);

                if (!misses.HasItem())
                    break;
            }
        }

        return list.HasItem() ? list : null;
    }

    public void Remove(string key, string group, CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            provider.Remove(key, group);
        }
    }

    public async Task RemoveAsync(string key, string group, CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            await provider.RemoveAsync(key, group);
        }
    }

    public void RemoveByGroup(string group, CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            provider.RemoveByGroup(group);
        }
    }

    public async Task RemoveByGroupAsync(string group, CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            await provider.RemoveByGroupAsync(group);
        }
    }

    public void Set(string key, string group, object data, DateTime expiry, CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            provider.Set(key, group, data, expiry);
        }
    }

    public async Task SetAsync(string key, string group, object data, DateTime expiry, CacheOptions options = null)
    {
        var providers = GetProvidersByProviderType(GetProviderTypeFromCacheOptions(options));

        foreach (var provider in providers)
        {
            await provider.SetAsync(key, group, data, expiry);
        }
    }

    private CacheProviderType GetProviderTypeFromCacheOptions(CacheOptions options = null)
    {
        if (options == null)
        {
            // Default
            var providerTypeFromConfig = "All";

            return providerTypeFromConfig.ToEnum<CacheProviderType>();
        }

        return options.ProviderType;
    }

    private List<ICacheProvider> GetProvidersByProviderType(CacheProviderType providerType)
    {
        var cacheProviders = new List<ICacheProvider>();

        if (_providers.HasItem() && _providers.ContainsKey(providerType))
        {
            return _providers.First(p => p.Key == providerType).Value;
        }

        lock (providerType.ToString())
        {
            switch (providerType)
            {
                case CacheProviderType.Local:
                    cacheProviders = GetAllProviders()
                        .Where(p =>
                            (CacheProviderType) p.GetType()
                                .GetProperty("ProviderType")
                                .GetValue(p, null) == CacheProviderType.Local)
                        .OrderBy(p => p.Priority)
                        .ToList();
                    break;

                case CacheProviderType.Shared:
                    cacheProviders = GetAllProviders()
                        .Where(p =>
                            (CacheProviderType) p.GetType()
                                .GetProperty("ProviderType")
                                .GetValue(p, null) == CacheProviderType.Shared)
                        .OrderBy(p => p.Priority)
                        .ToList();
                    break;

                case CacheProviderType.All:
                    cacheProviders = GetAllProviders()
                        //.Where(p => (CacheProviderType)(p.GetType().GetProperty("ProviderType").GetValue(p, null)) == CacheProviderType.All)
                        .OrderBy(p => p.Priority)
                        .ToList();
                    break;
            }

            return _providers.GetOrAdd(providerType, cacheProviders);
        }
    }

    private List<ICacheProvider> GetAllProviders()
    {
        return _serviceProvider.GetServices<ICacheProvider>().ToList();
    }
}