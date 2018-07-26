using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace OOPCachingSpeedTest.Cache.Providers
{
    public class InMemoryContext : ICacheContext
    {
        public Task<T> Get<T>(string cacheKey) where T : class
        {
            return Task.FromResult((T) MemoryCache.Default.Get(cacheKey));
        }

        public Task Add(string cacheKey, object value, TimeSpan expiryDuration)
        {
            MemoryCache.Default.Add(cacheKey, value, DateTime.Now + expiryDuration);
            return Task.CompletedTask;
        }
    }
}