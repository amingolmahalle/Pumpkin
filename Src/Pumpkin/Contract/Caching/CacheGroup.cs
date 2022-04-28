namespace Pumpkin.Contract.Caching;

public class CacheGroup
{
    public string Group { get; set; }

    public IEnumerable<string> Keys { get; set; }

    public CacheOptions Options { get; set; }
}