using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OOPCachingSpeedTest.Checksum;

namespace OOPCachingSpeedTest.Cache
{
    public class VersionedCache
    {
        private readonly Dictionary<CacheDuration, int> _cacheDurationLookup;
        private readonly ICacheContext _cacheContext;
        private readonly IClassChecksumCalculator _classChecksum;

        public VersionedCache(ICacheContext cacheContext, IClassChecksumCalculator classChecksum)
        {
            _cacheContext = cacheContext;
            _classChecksum = classChecksum;
            _cacheDurationLookup = new Dictionary<CacheDuration, int>
            {
                {CacheDuration.Short, 5}, //5 Minutes
                {CacheDuration.Default, 30}, //Half an hour
                {CacheDuration.Long, 60}, //Hour
                {CacheDuration.VeryLong, 1440}, //Day
            };
        }

        public async Task<T> Get<T>(string cacheKey) where T : class
        {
            return await _cacheContext.Get<T>(GetVersionedCacheKey(cacheKey, typeof(T))); }

        public async Task Add(string cacheKey, object value, CacheDuration cacheDuration, Type cacheItemType)
        {
            await _cacheContext.Add(GetVersionedCacheKey(cacheKey, cacheItemType), value, GetDurationMinutes(cacheDuration));
        }

        private string GetVersionedCacheKey(string cacheKey, Type type)
        {
            return $"{cacheKey}:{_classChecksum.GetHash(type)}";
        }

        private TimeSpan GetDurationMinutes(CacheDuration cacheDuration)
        {
            return TimeSpan.FromMinutes(_cacheDurationLookup.ContainsKey(cacheDuration) ? _cacheDurationLookup[cacheDuration] : 0);
        }

    }
}