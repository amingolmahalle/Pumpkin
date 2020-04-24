using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pumpkin.Contract.Caching;
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
            var resultFromCache = _connectionFactory.GetRandomDatabase().Database.StringGet(CreateKey(key, group));
            if (resultFromCache == default)
                return default;
            return _serializer.Deserialize<T>(resultFromCache);
        }

        public async Task<T> GetAsync<T>(string key, string group)
        {
            var resultFromCache =
                await _connectionFactory.GetRandomDatabase().Database.StringGetAsync(CreateKey(key, group));
            if (resultFromCache == default)
                return default;
            return _serializer.Deserialize<T>(resultFromCache);
        }

        public IDictionary<string, T> GetMany<T>(IEnumerable<string> keys, string group)
            where T : class, new()
        {
            var result = new Dictionary<string, T>();
            var keysForRedis = keys.Select(p => (RedisKey) CreateKey(p, group)).ToArray();
            var resultFromCache = _connectionFactory.GetRandomDatabase().Database.StringGet(keysForRedis);
            if (keysForRedis.HasItem() && resultFromCache.HasItem())
            {
                var keysArray = keys.ToArray();
                for (int i = 0; i < keysForRedis.Length; i++)
                {
                    result.Add(keysArray[i],
                        resultFromCache[i] != default
                            ? _serializer.Deserialize<T>(resultFromCache[i])
                            : null);
                }
            }

            return result;
        }

        public IDictionary<string, object> GetMany(IEnumerable<string> keys, string group)
        {
            var result = new Dictionary<string, object>();
            var keysForRedis = keys.Select(p => (RedisKey) CreateKey(p, group)).ToArray();
            var resultFromCache = _connectionFactory.GetRandomDatabase().Database.StringGet(keysForRedis);

            if (keysForRedis.HasItem() && resultFromCache.HasItem())
            {
                var keysArray = keys.ToArray();
                for (int i = 0; i < keysForRedis.Length; i++)
                {
                    result.Add(keysArray[i],
                        resultFromCache[i] != default
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
            var keysForRedis = keys.Select(p => (RedisKey) CreateKey(p, group)).ToArray();
            var resultFromCache = await _connectionFactory.GetRandomDatabase().Database.StringGetAsync(keysForRedis);
            if (keysForRedis.HasItem() && resultFromCache.HasItem())
            {
                var keysArray = keys.ToArray();
                for (int i = 0; i < keysForRedis.Length; i++)
                {
                    result.Add(keysArray[i],
                        resultFromCache[i] != default
                            ? _serializer.Deserialize<T>(resultFromCache[i])
                            : null);
                }
            }

            return result;
        }

        public async Task<IDictionary<string, object>> GetManyAsync(IEnumerable<string> keys, string group)
        {
            var result = new Dictionary<string, object>();
            var enumerable = keys.ToList();
            var keysForRedis = enumerable.Select(p => (RedisKey) CreateKey(p, group)).ToArray();
            var resultFromCache = await _connectionFactory.GetRandomDatabase().Database.StringGetAsync(keysForRedis);
            if (keysForRedis.HasItem() && resultFromCache.HasItem())
            {
                var keysArray = enumerable.ToArray();
                for (int i = 0; i < keysForRedis.Length; i++)
                {
                    result.Add(keysArray[i],
                        resultFromCache[i] != default
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
                _connectionFactory.GetMaster().Database.KeyDelete(CreateKey(key, group));
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
                await _connectionFactory.GetMaster().Database.KeyDeleteAsync(CreateKey(key, group));
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
                var keyDeleteAsync = await _connectionFactory.GetMaster()
                    .Database
                    .KeyDeleteAsync(keyList.ToArray());
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
                _connectionFactory.GetMaster().Database.StringSet(CreateKey(key, group), _serializer.Serialize(data),
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
                await _connectionFactory.GetMaster().Database.StringSetAsync(CreateKey(key, group),
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

        private string CreateKey(string key, string group)
        {
            return $"{group}::{key}";
        }
    }
}