using StackExchange.Redis;

namespace Infrastructure.Framework.Caching.Providers.Shared.Redis;

public class RedisConnectionEntry
{
    public IDatabase Database { get; set; }
        
    public IConnectionMultiplexer Connection { get; set; }
        
    public IServer Server { get; set; }
        
    public int Index { get; set; }
}