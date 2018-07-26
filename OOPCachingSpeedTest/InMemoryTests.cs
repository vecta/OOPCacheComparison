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
            await new Tests().TenThousandReads(new InMemoryContext());
        }

        [Test]
        public async Task TenThousandWrites()
        {
            await new Tests().TenThousandWrites(new InMemoryContext());
        }
    }
}
