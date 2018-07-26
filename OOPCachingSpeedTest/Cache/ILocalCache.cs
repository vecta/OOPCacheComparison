using System;

namespace OOPCachingSpeedTest.Cache
{
    internal interface ILocalCache
    {
        T Get<T>(string key);
        void Add(string key, object value, DateTimeOffset cacheDuration);
    }
}