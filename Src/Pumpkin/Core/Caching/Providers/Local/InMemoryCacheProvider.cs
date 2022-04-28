using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using Pumpkin.Contract.Caching;

namespace Pumpkin.Core.Caching.Providers.Local;

public class InMemoryCacheProvider : ICacheProvider
{
    private readonly IMemoryCache _cacheEngine;

    private static ConcurrentDictionary<string, string> Keys { get; } = new();

    public InMemoryCacheProvider(IMemoryCache cacheEngine)
    {
        _cacheEngine = cacheEngine;
    }

    public byte Priority => 0;
    public CacheProviderType ProviderType => CacheProviderType.Local;
    public Func<string, object> SerializerMethod => null;
    public Func<object, string> DeserializerMethod => null;

    public void Set(string key, string group, object data, DateTime expiry)
    {
        var _key = createKey(key, group);
            
        _cacheEngine.Set(_key, data, new DateTimeOffset(expiry));
            
        Keys.TryAdd(_key, _key);
    }

    public async Task SetAsync(string key, string group, object data, DateTime expiry)
    {
        await Task.Run(() => Set(key, group, data, expiry));
    }

    public T Get<T>(string key, string group)
    {
        var cachedItem = _cacheEngine.Get(createKey(key, group));
        return cachedItem != null ? (T) cachedItem : default(T);
    }

    public IDictionary<string, object> GetMany(IEnumerable<string> keys, string group)
    {
        // fuck .Net Core !!!!
        //return _cacheEngine.GetValues(keys.Select(p => createKey(p, group)));
        var res = new Dictionary<string, object>();
        foreach (var key in keys)
        {
            res.Add(createKey(key, group), Get<object>(key, group));
        }

        return res;
    }

    public IDictionary<string, T> GetMany<T>(IEnumerable<string> keys, string group) where T : class, new()
    {
        var res = new Dictionary<string, T>();
        foreach (var key in keys)
        {
            res.Add(createKey(key, group), Get<T>(key, group));
        }

        return res;
    }


    public void Remove(string key, string group)
    {
        var _key = createKey(key, group);
        _cacheEngine.Remove(_key);
    }

    public async Task RemoveAsync(string key, string group)
    {
        await Task.Run(() => Remove(key, group));
    }

    public void RemoveByGroup(string group)
    {
        var keys = getAllKeysBygroup(group);
        foreach (var key in keys)
        {
            _cacheEngine.Remove(key);
            Keys.Remove(key, out var deleted);
        }
    }

    public async Task RemoveByGroupAsync(string group)
    {
        await Task.Run(() => RemoveByGroup(group));
    }

    public void Flush()
    {
        var keys = getAllKeys();
        foreach (var key in keys)
        {
            _cacheEngine.Remove(key.Key);
        }

        Keys.Clear();
    }

    public async Task FlushAsync()
    {
        await Task.Run(() => Flush());
    }

    public async Task<T> GetAsync<T>(string key, string group)
    {
        return await Task.Run(() => Get<T>(key, @group));
    }

    public async Task<IDictionary<string, object>> GetManyAsync(IEnumerable<string> keys, string group)
    {
        return await Task.Run(() => GetMany(keys, @group));
    }

    public async Task<IDictionary<string, T>> GetManyAsync<T>(IEnumerable<string> keys, string group)
        where T : class, new()
    {
        return await Task.Run(() => GetMany<T>(keys, @group));
    }

    private string createKey(string key, string group)
    {
        return $"{group}::{key}";
    }

    /// <summary> use with CAUTION </summary>
    private ConcurrentDictionary<string, string> getAllKeys()
    {
        return Keys;
    }

    private List<string> getAllKeysBygroup(string groupName)
    {
        return Keys.Keys.Where(p => p.StartsWith($"{groupName}:")).ToList();
    }

    public T GetOrCreate<T>(string key, string group, DateTime expiry, Func<T> method)
    {
        var cacheResponse = Get<T>(key, group);
        if (cacheResponse != null && !cacheResponse.Equals(default(T)))
            return cacheResponse;
        var actualResponse = method.Invoke();
        Set(key, group, actualResponse, expiry);
        return actualResponse;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, string group, DateTime expiry, Func<Task<T>> method)
    {
        var cacheResponse = await GetAsync<T>(key, group);
        if (cacheResponse != null && !cacheResponse.Equals(default(T)))
            return cacheResponse;
        var actualResponse = await method.Invoke();
        await SetAsync(key, group, actualResponse, expiry);
        return actualResponse;
    }
}