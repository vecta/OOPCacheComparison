using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace OOPCachingSpeedTest.Cache.Providers
{
    //https://www.howtogeek.com/249966/how-to-install-and-use-the-linux-bash-shell-on-windows-10/
    //https://www.digitalocean.com/community/tutorials/how-to-install-and-use-redis
    public class RedisCacheContext : ICacheContext
    {
        private readonly IDatabase _database;

        public RedisCacheContext(IDatabase database) { _database = database; }

        public async Task<T> Get<T>(string cacheKey) where T : class
        {
            var cachedValue = await _database.StringGetAsync(cacheKey);
            return DeserializeValue<T>(cachedValue);
        }

        public async Task Add(string cacheKey, object value, TimeSpan expiryDuration)
        {
            if (value == null)
                return;

            await _database.StringSetAsync(cacheKey, SearializeValue(value), expiryDuration);
        }

        private T DeserializeValue<T>(string value) where T: class
        {
            return value == null ? null : JsonConvert.DeserializeObject<T>(value);
        }

        private RedisValue SearializeValue(object value)
        {
            return (RedisValue)JsonConvert.SerializeObject(value);
        }
    }
}