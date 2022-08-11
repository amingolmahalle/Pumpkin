namespace Pumpkin.Domain.Framework.Caching;

public interface ICacheService
{
    void Set(string key, string group, object data, DateTime expiry, CacheOptions options = null);

    Task SetAsync(string key, string group, object data, DateTime expiry, CacheOptions options = null);

    T Get<T>(string key, string group, CacheOptions options = null);

    Task<T> GetAsync<T>(string key, string group, CacheOptions options = null);

    T GetOrCreate<T>(string key, string group, DateTime expiry, Func<T> method, CacheOptions options = null);

    Task<T> GetOrCreateAsync<T>(string key, string group, DateTime expiry, Func<Task<T>> method, CacheOptions options = null);

    IDictionary<string, object> GetMany(IEnumerable<string> keys, string group, CacheOptions options = null);

    Task<IDictionary<string, object>> GetManyAsync(IEnumerable<string> keys, string group, CacheOptions options = null);

    IDictionary<string, T> GetMany<T>(IEnumerable<string> keys, string group, CacheOptions options = null) where T : class, new();

    Task<IDictionary<string, T>> GetManyAsync<T>(IEnumerable<string> keys, string group, CacheOptions options = null) where T : class, new();

    void Remove(string key, string group, CacheOptions options = null);

    Task RemoveAsync(string key, string group, CacheOptions options = null);

    void RemoveByGroup(string group, CacheOptions options = null);

    Task RemoveByGroupAsync(string group, CacheOptions options = null);
}