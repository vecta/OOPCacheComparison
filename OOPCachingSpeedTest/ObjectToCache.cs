using System;

namespace OOPCachingSpeedTest
{
    [Serializable]
    public class ObjectToCache
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime Expiry { get; }
        public DateTime Created { get; }
        public string Description { get; }
        public ChildObjectToCache ChildOne { get; }
        public ChildObjectToCache ChildTwo { get; }

        public ObjectToCache()
        {
            Id = 17;
            Name = "Marmaduke";
            Expiry = new DateTime(1976, 1, 11);
            Created = new DateTime(2010, 1, 16);
            Description = "Test object for serialization";
            ChildOne = new ChildObjectToCache();
            ChildTwo = new ChildObjectToCache();
        }
    }
}