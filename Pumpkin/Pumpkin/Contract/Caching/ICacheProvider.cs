using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pumpkin.Contract.Caching
{
    public interface ICacheProvider
    {
        CacheProviderType ProviderType { get; }
        
        byte Priority { get; }
        
        void Set(string key, string group, object data, DateTime expiry);
        
        Task SetAsync(string key, string group, object data, DateTime expiry);
        
        T Get<T>(string key, string group);
        
        Task<T> GetAsync<T>(string key, string group);
        
        T GetOrCreate<T>(string key, string group, DateTime expiry, Func<T> method);
        
        Task<T> GetOrCreateAsync<T>(string key, string group, DateTime expiry, Func<Task<T>> method);
        
        IDictionary<string, object> GetMany(IEnumerable<string> keys, string group);
        
        Task<IDictionary<string, object>>  GetManyAsync(IEnumerable<string> keys, string group);
        
        IDictionary<string, T> GetMany<T>(IEnumerable<string> keys, string group) where T: class ,new();
        
        Task<IDictionary<string, T>> GetManyAsync<T>(IEnumerable<string> keys, string group) where T : class, new();
        
        void Remove(string key, string group);
        
        Task RemoveAsync(string key, string group);
        
        void RemoveByGroup(string group);
        
        Task RemoveByGroupAsync(string group);
        
        void Flush();
        
        Task FlushAsync();
    }
}
