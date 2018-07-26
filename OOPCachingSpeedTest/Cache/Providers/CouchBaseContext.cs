using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Couchbase.Core;

namespace OOPCachingSpeedTest.Cache.Providers
{
    public class CouchBaseContext : ICacheContext
    {
        private readonly IBucket _bucket;
        private readonly Cluster _cluster;

        public CouchBaseContext()
        {
            _cluster = new Cluster(new ClientConfiguration
            {
                Servers = new List<Uri> {new Uri("http://127.0.0.1")}
            });

            _cluster.Authenticate(new PasswordAuthenticator("Administrator", "fireblade"));
            _bucket = _cluster.OpenBucket("Cache");
        }

        public async Task<T> Get<T>(string cacheKey) where T : class
        {
            var result = await _bucket.GetAsync<T>(cacheKey);
            return result.Value;
        }

        public async Task Add(string cacheKey, object value, TimeSpan expiryDuration)
        {
            await _bucket.UpsertAsync(cacheKey, value, expiryDuration);
        }

        ~CouchBaseContext()
        {
            _cluster.CloseBucket(_bucket);
        }
    }
}