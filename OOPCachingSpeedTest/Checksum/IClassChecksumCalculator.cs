using System;

namespace OOPCachingSpeedTest.Checksum
{
    public interface IClassChecksumCalculator
    {
        string GetHash(Type t);
    }
}