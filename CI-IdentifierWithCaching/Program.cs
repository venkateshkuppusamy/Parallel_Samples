using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CI_IdentifierWithCaching
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CI identifier with caching...");
        }


    }


    class CIReceiver {

        public void Start()
        {
                        

        }

    }

    class CICache {
        public static IAppCache cache = new CachingService();
        static MemoryCacheEntryOptions policy;

        static CICache()
        {
            PostEvictionDelegate del = new PostEvictionDelegate(OnRemovalCallBack);
            policy = new MemoryCacheEntryOptions();
            policy.RegisterPostEvictionCallback(del);
        }
        private static void OnRemovalCallBack(object key, object value, EvictionReason reason, object state)
        {
            Console.WriteLine($"CIevents count {((List<string>)value).Count }");
            Console.WriteLine($"Cache key :{(string)key} {reason.ToString()}");
            Console.WriteLine($"DB input:  [{string.Join(',', ((List<string>)value))}]");

            // Code to publish data to db and EH
            // Batch insert db and batch publish messages.

        }

        public static List<string> GetOrAdd(string cacheItem,CIEvent ciEvent)
        {
            //policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(1);
            return cache.GetOrAdd(cacheItem, () => { Console.WriteLine("Get 8 hrs range data from DB based on ciEvent"); return new List<string>(); }, policy);
        }
        public static List<string> Get(string cacheItem)
        {
            return cache.Get<List<string>>(cacheItem);
        }
    }

    class CIEvent
    {
        public bool IsDirty { get; set; }

        private int _ID;
        public int ID { get{ return _ID; } set { _ID = value; IsDirty = true; } }
        public string AssetDSN { get; set; }
        public string CycleID { get; set; }
        public string  NextCycleID { get; set; }
        public DateTime assetLocationTime { get; set; }

        
    }
}
