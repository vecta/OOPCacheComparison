using System;
using System.Threading.Tasks;

namespace OOPCachingSpeedTest.Cache
{
    public interface ICacheContext
    {
        Task<T> Get<T>(string cacheKey) where T : class;
        Task Add(string cacheKey, object value, TimeSpan expiryDuration);
    }
}