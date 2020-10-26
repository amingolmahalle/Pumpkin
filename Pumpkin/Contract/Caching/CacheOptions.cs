namespace Pumpkin.Contract.Caching
{
    public class CacheOptions
    {
        public  CacheProviderType ProviderType { get; }

        public CacheOptions(CacheProviderType providerType)
        {
            ProviderType = providerType;
        }
    }
}
