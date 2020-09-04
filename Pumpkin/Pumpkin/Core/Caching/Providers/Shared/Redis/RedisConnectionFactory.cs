using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Pumpkin.Core.Caching.Providers.Shared.Redis
{
    public class RedisConnectionFactory
    {
        private List<RedisConnectionEntry> _cacheDatabases;

        private readonly IConfiguration _configuration;

        private string[] _nodesArray;

        public RedisConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            if (_cacheDatabases != null)
            {
                foreach (var item in _cacheDatabases)
                {
                    item.Connection.Dispose();
                }
            }

            _cacheDatabases = new List<RedisConnectionEntry>();

            var nodes = _configuration.GetConnectionString("Redis");

            _nodesArray = nodes.Split(',');

            for (int i = 0; i < _nodesArray.Count(); i++)
            {
                var connString = GetConnectionString(i);

                IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(connString);

                var db = connectionMultiplexer.GetDatabase();

                var serverIpDate = _nodesArray[i].Split(':');

                IServer server = connectionMultiplexer.GetServer(serverIpDate[0] + ":" +
                                                                 serverIpDate[1]);

                _cacheDatabases.Add(new RedisConnectionEntry()
                {
                    Server = server,
                    Connection = connectionMultiplexer,
                    Database = db,
                    Index = i
                });
            }
        }

        private string GetConnectionString(int index)
        {
            const string extraConfigs = "allowAdmin=true,abortConnect=false,connectRetry=5,connectTimeout=500";

            var selected = _nodesArray[index];
            var servers = selected;
            var connectionString = $"{servers},{extraConfigs}";

            return connectionString;
        }

        public RedisConnectionEntry GetMaster()
        {
            return _cacheDatabases.SingleOrDefault(it => it.Index == 0)
                   ?? new RedisConnectionEntry();
        }

        public RedisConnectionEntry GetRandomDatabase()
        {
            var index = new Random().Next(0, _nodesArray.Count());

            return _cacheDatabases.SingleOrDefault(it => it.Index == index)
                   ?? new RedisConnectionEntry();
        }
    }
}