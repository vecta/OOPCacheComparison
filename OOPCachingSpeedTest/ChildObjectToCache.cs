using System;
using System.Collections.Generic;

namespace OOPCachingSpeedTest
{
    [Serializable]
    public class ChildObjectToCache
    {
        public List<int> Sizes { get; }
        public int Id { get; }
        public string Name { get; }

        public ChildObjectToCache()
        {
            Sizes = new List<int> { 3, 5, 7, 9 };
            Id = 11;
            Name = "Trevor";
        }
    }
}