using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Caching;
using Pumpkin.Contract.Registration;
using Pumpkin.Contract.Serialization;
using Pumpkin.Utils.Extensions;
using StackExchange.Redis;

namespace Pumpkin.Core.Caching.Providers.Shared.Redis
{
    public class RedisCacheProvider : ICacheProvider
    {
        public byte Priority => 1;

        private readonly RedisConnectionFactory _connectionFactory;

        private readonly ISerializer _serializer;

        //  private static ILog _logger;

        public CacheProviderType ProviderType => CacheProviderType.Shared;

        public RedisCacheProvider(RedisConnectionFactory connectionFactory, ISerializer serializer)
        {
            _connectionFactory = connectionFactory;
            //   _logger = LogManager.GetLogger(typeof(RedisCacheProvider));
            _serializer = serializer;
        }

        public T Get<T>(string key, string group)
        {
            var resultFromCache = _connectionFactory.GetRandomDatabase().Database.StringGet(createKey(key, group));
            if (resultFromCache == default(RedisValue))
                return default(T);
            return _serializer.Deserialize<T>(resultFromCache);
        }

        public async Task<T> GetAsync<T>(string key, string group)
        {
            var resultFromCache =
                await _connectionFactory.GetRandomDatabase().Database.StringGetAsync(createKey(key, group));
            if (resultFromCache == default(RedisValue))
                return default(T);
            return _serializer.Deserialize<T>(resultFromCache);
        }

        public IDictionary<string, T> GetMany<T>(IEnumerable<string> keys, string group)
            where T : class, new()
        {
            var result = new Dictionary<string, T>();
            var keysForRedis = keys.Select(p => (RedisKey) createKey(p, group)).ToArray();
            var resultFromCache = _connectionFactory.GetRandomDatabase().Database.StringGet(keysForRedis);
            if (keysForRedis.HasItem() && resultFromCache.HasItem())
            {
                var keysArray = keys.ToArray();
                for (int i = 0; i < keysForRedis.Length; i++)
                {
                    result.Add(keysArray[i],
                        resultFromCache[i] != default(RedisValue)
                            ? _serializer.Deserialize<T>(resultFromCache[i])
                            : null);
                }
            }

            return result;
        }

        public IDictionary<string, object> GetMany(IEnumerable<string> keys, string group)
        {
            var result = new Dictionary<string, object>();
            var keysForRedis = keys.Select(p => (RedisKey) createKey(p, group)).ToArray();
            var resultFromCache = _connectionFactory.GetRandomDatabase().Database.StringGet(keysForRedis);
            if (keysForRedis.HasItem() && resultFromCache.HasItem())
            {
                var keysArray = keys.ToArray();
                for (int i = 0; i < keysForRedis.Length; i++)
                {
                    result.Add(keysArray[i],
                        resultFromCache[i] != default(RedisValue)
                            ? _serializer.Deserialize<object>(resultFromCache[i])
                            : null);
                }
            }

            return result;
        }

        public async Task<IDictionary<string, T>> GetManyAsync<T>(IEnumerable<string> keys, string group)
            where T : class, new()
        {
            var result = new Dictionary<string, T>();
            var keysForRedis = keys.Select(p => (RedisKey) createKey(p, group)).ToArray();
            var resultFromCache = await _connectionFactory.GetRandomDatabase().Database.StringGetAsync(keysForRedis);
            if (keysForRedis.HasItem() && resultFromCache.HasItem())
            {
                var keysArray = keys.ToArray();
                for (int i = 0; i < keysForRedis.Length; i++)
                {
                    result.Add(keysArray[i],
                        resultFromCache[i] != default(RedisValue)
                            ? _serializer.Deserialize<T>(resultFromCache[i])
                            : null);
                }
            }

            return result;
        }

        public async Task<IDictionary<string, object>> GetManyAsync(IEnumerable<string> keys, string group)
        {
            var result = new Dictionary<string, object>();
            var keysForRedis = keys.Select(p => (RedisKey) createKey(p, group)).ToArray();
            var resultFromCache = await _connectionFactory.GetRandomDatabase().Database.StringGetAsync(keysForRedis);
            if (keysForRedis.HasItem() && resultFromCache.HasItem())
            {
                var keysArray = keys.ToArray();
                for (int i = 0; i < keysForRedis.Length; i++)
                {
                    result.Add(keysArray[i],
                        resultFromCache[i] != default(RedisValue)
                            ? _serializer.Deserialize<object>(resultFromCache[i])
                            : null);
                }
            }

            return result;
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

        public void Remove(string key, string group)
        {
            try
            {
                _connectionFactory.GetMaster().Database.KeyDelete(createKey(key, group));
            }
            catch (Exception ex)
            {
                //   _logger.Fatal(ex.Message, ex);

                throw ex;
            }
        }

        public async Task RemoveAsync(string key, string group)
        {
            try
            {
                await _connectionFactory.GetMaster().Database.KeyDeleteAsync(createKey(key, group));
            }
            catch (Exception ex)
            {
                //    _logger.Fatal(ex.Message, ex);

                throw ex;
            }
        }

        public void RemoveByGroup(string group)
        {
            try
            {
                var keyList = _connectionFactory.GetMaster().Server.Keys(pattern: $"{group}:*");
                var keyDeleteAsync = _connectionFactory.GetMaster().Database.KeyDelete(keyList.ToArray());
            }
            catch (Exception ex)
            {
                //   _logger.Fatal(ex.Message, ex);

                throw ex;
            }
        }

        public async Task RemoveByGroupAsync(string group)
        {
            try
            {
                var keyList = _connectionFactory.GetMaster().Server.Keys(pattern: $"{group}:*");
                var keyDeleteAsync = await _connectionFactory.GetMaster().Database.KeyDeleteAsync(keyList.ToArray());
            }
            catch (Exception ex)
            {
                //   _logger.Fatal(ex.Message, ex);

                throw ex;
            }
        }

        public void Set(string key, string group, object data, DateTime expiry)
        {
            try
            {
                _connectionFactory.GetMaster().Database.StringSet(createKey(key, group), _serializer.Serialize(data),
                    expiry.TimeOfDay);
            }
            catch (Exception ex)
            {
                //      _logger.Error(ex.Message, ex);

                throw ex;
            }
        }

        public async Task SetAsync(string key, string group, object data, DateTime expiry)
        {
            try
            {
                await _connectionFactory.GetMaster().Database.StringSetAsync(createKey(key, group),
                    _serializer.Serialize(data), expiry.TimeOfDay);
            }
            catch (Exception ex)
            {
                //    _logger.Error(ex.Message, ex);

                throw ex;
            }
        }

        public void Flush()
        {
            _connectionFactory.GetMaster().Server.FlushAllDatabases();
        }

        public async Task FlushAsync()
        {
            await _connectionFactory.GetMaster().Server.FlushAllDatabasesAsync();
        }

        private string createKey(string key, string group)
        {
            return $"{group}::{key}";
        }
    }
}