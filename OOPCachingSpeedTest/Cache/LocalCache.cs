using System;
using System.Runtime.Caching;

namespace OOPCachingSpeedTest.Cache
{
    class LocalCache : ILocalCache
    {
        public T Get<T>(string key)
        {
            return (T)MemoryCache.Default.Get(key);
        }

        public void Add(string key, object value, DateTimeOffset cacheDuration)
        {
            MemoryCache.Default.Add(key, value, cacheDuration);
        }
    }
}