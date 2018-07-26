using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using OOPCachingSpeedTest.Cache;

namespace OOPCachingSpeedTest.Checksum
{
    class ClassChecksumCalculator : IClassChecksumCalculator
    {
        private readonly ILocalCache _cache;

        public ClassChecksumCalculator(ILocalCache cache)
        {
            _cache = cache;
        }

        public string GetHash(Type t)
        {
            var followedProperties = new HashSet<string>();
            var cacheKey = "ClassHash:" + t.FullName;
            var cachedValue = _cache.Get<string>(cacheKey);
            if (cachedValue != null)
                return cachedValue;
            var hash = GetHashCode(GetDefinition(t, followedProperties));
            _cache.Add(cacheKey, hash, DateTimeOffset.MaxValue);
            return hash;
        }

        private string GetDefinition(Type type, HashSet<string> followedProperties)
        {
            var members = FormatterServices.GetSerializableMembers(type);

            var classDefinition = type.FullName;
            foreach (var memberInfo in members)
            {
                var memberType = MemberType(memberInfo);
                var memberDefinition = $"{memberInfo.Name}:{memberType.Name};";
                classDefinition += memberDefinition;
                if (IsOneOfOurClasses(memberInfo, memberType) && !followedProperties.Contains(memberDefinition))
                {
                    followedProperties.Add(memberDefinition);
                    classDefinition += GetDefinition(memberType, followedProperties);
                }
            }

            return classDefinition;
        }

        private string GetHashCode(string classDefinition)
        {
            var hashBytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(classDefinition));
            return string.Join("", hashBytes.Select(b => b.ToString("X2")));
        }

        private bool IsOneOfOurClasses(MemberInfo memberInfo, Type memberType)
        {
            return memberInfo.ReflectedType != null && memberInfo.ReflectedType.IsClass && !memberType.FullName.StartsWith("System.");
        }

        private Type MemberType(MemberInfo memberInfo)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)memberInfo).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo).PropertyType;
            }
            return null;
        }
    }
}