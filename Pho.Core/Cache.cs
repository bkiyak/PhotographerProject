using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Pho.Core
{
    public class Cache
    {
    }
    public class CacheKey
    {
        private readonly string _format = "{0}-{1}";
        private readonly string _generatedKey;

        public CacheKey(EntitiesEnum entity, string entityId)
        {
            _generatedKey = string.Format(_format, entity.ToString(), entityId.ToString());
        }

        public static CacheKey New(EntitiesEnum entity, string entityId)
        {
            return new CacheKey(entity, entityId);
        }

        public override string ToString()
        {
            return _generatedKey;
        }
    }

    public enum EntitiesEnum
    {
        Content,
        User,
        Picture,
        Content_Picture_Mapping,
        Menu,
        Description
    }
    public abstract class CacheProvider
    {
        public static int CacheDuration = 60;
        public static CacheProvider Instance { get; set; }

        public abstract object Get(CacheKey key);
        public abstract void Set(CacheKey key, object value);
        public abstract bool IsExist(CacheKey key);
        public abstract void Remove(CacheKey key);
    }
    public class DefaultCacheProvider : CacheProvider
    {
        private ObjectCache _cache = null;
        private CacheItemPolicy _policy = null;

        public DefaultCacheProvider()
        {
            Trace.WriteLine("Cache Initialize Oldu!");

            _cache = MemoryCache.Default;
            _policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(CacheDuration),
                RemovedCallback = new CacheEntryRemovedCallback(CacheRemovedCallback)
            };
        }

        private static void CacheRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            Trace.WriteLine("----------Cache Expire Oldu----------");
            Trace.WriteLine("Key : " + arguments.CacheItem.Key);
            Trace.WriteLine("Value : " + arguments.CacheItem.Value.ToString());
            Trace.WriteLine("RemovedReason : " + arguments.RemovedReason);
            Trace.WriteLine("-------------------------------------");
        }

        public override object Get(CacheKey key)
        {
            object retVal = null;

            try
            {
                retVal = _cache.Get(key.ToString());
            }
            catch (Exception e)
            {
                Trace.WriteLine("Hata : CacheProvider.Get()\n" + e.Message);
                throw new Exception("Cache Get sırasında bir hata oluştu!", e);
            }

            return retVal;
        }

        public override void Set(CacheKey key, object value)
        {
            try
            {
                Trace.WriteLine("Cache Setleniyor. Key : " + key.ToString());
                _cache.Set(key.ToString(), value, _policy);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Hata : CacheProvider.Set()\n" + e.Message);
                throw new Exception("Cache Set sırasında bir hata oluştu!", e);
            }
        }

        public override bool IsExist(CacheKey key)
        {
            return _cache.Any(q => q.Key == key.ToString());
        }

        public override void Remove(CacheKey key)
        {
            _cache.Remove(key.ToString());
        }
    }

}
