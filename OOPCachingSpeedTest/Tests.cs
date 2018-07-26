using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using OOPCachingSpeedTest.Cache;
using OOPCachingSpeedTest.Checksum;

namespace OOPCachingSpeedTest
{
    public class Tests
    {
        public async Task TenThousandReads(ICacheContext context)
        {
            const string cacheKey = "cacheKey";
            var stopwatch = new Stopwatch();
            var objectToCache = new ObjectToCache();
            var cache = new VersionedCache(context, new ClassChecksumCalculator(new LocalCache()));
            await cache.Add(cacheKey, objectToCache, CacheDuration.Default, objectToCache.GetType());
            stopwatch.Start();

            var tasks = new List<Task<ObjectToCache>>();
            for (var i = 0; i < 10000; i++)
            {
                tasks.Add(cache.Get<ObjectToCache>(cacheKey));
            }

            await Task.WhenAll(tasks);

            stopwatch.Stop();
            Console.WriteLine("Time: " + stopwatch.ElapsedMilliseconds);
        }

  
        public async Task TenThousandWrites(ICacheContext context)
        {
            var stopwatch = new Stopwatch();
            var cache = new VersionedCache(context, new ClassChecksumCalculator(new LocalCache()));

            var tasks = new List<Task>();
            stopwatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                tasks.Add(cache.Add($"cacheKey:{i}", new ObjectToCache(), CacheDuration.Default,
                    new ObjectToCache().GetType()));
            }
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine("Time: " + stopwatch.ElapsedMilliseconds);
        }
    }
}