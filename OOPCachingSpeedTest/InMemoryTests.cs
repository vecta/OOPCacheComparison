using System.Threading.Tasks;
using NUnit.Framework;
using OOPCachingSpeedTest.Cache.Providers;

namespace OOPCachingSpeedTest
{
    [TestFixture]
    public class InMemoryTests
    {
        [Test]
        public async Task TenThousandReads()
        {
            await Tests.TenThousandReads(new InMemoryContext());
        }

        [Test]
        public async Task TenThousandWrites()
        {
            await Tests.TenThousandWrites(new InMemoryContext());
        }
    }
}
