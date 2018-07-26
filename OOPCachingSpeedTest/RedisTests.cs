using System.Threading.Tasks;
using NUnit.Framework;
using OOPCachingSpeedTest.Cache.Providers;
using StackExchange.Redis;

namespace OOPCachingSpeedTest
{
    [TestFixture]
    public class RedisTests
    {
        [Test]
        public async Task TenThousandReads()
        {
            await Tests.TenThousandReads(new RedisCacheContext(ConnectionMultiplexer.Connect("127.0.0.1").GetDatabase()));
        }

        [Test]
        public async Task TenThousandWrites()
        {
            await Tests.TenThousandWrites(new RedisCacheContext(ConnectionMultiplexer.Connect("127.0.0.1").GetDatabase()));
        }
    }
}
