using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CIReceiver_BatchInsertWithCaching
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CI receiver  Batch Insert with Caching...");
            CICache.GetOrAdd("CIEvents");
            CIReceiver receiver = new CIReceiver();
            CIReceiver receiver2 = new CIReceiver();
            Task.Run(() => receiver2.Start(1, 50));
            receiver.Start(51, 100);
            Console.WriteLine("End of Program. Press any key to continue...");
            Console.ReadLine();
        }
    }

    class CIReceiver {
        public void Start(int counter, int msgCount)
        {
            int i = counter;
            while (i < msgCount)
            {
                Console.WriteLine("Thread ID: "+ Thread.CurrentThread.ManagedThreadId);
                //Add to Cache list
                var ciEvents = CICache.GetOrAdd("CIEvents");
                if (ciEvents != null)
                {
                    // do validation of the message.
                    ciEvents.Add(i.ToString());
                    Console.WriteLine(ciEvents.Count + " added to the cache list");
                
                    //if Cache list > 10 
                    if (ciEvents.Count > 10)
                    {
                        //post the message to db and EH    
                        CICache.cache.Remove("CIEvents");
                        if (CICache.cache.Get<List<string>>("CIEvents") == null)
                            Console.WriteLine("CI Events key removed...");
                    }
                }
                i++;
                Thread.Sleep(300);
            }
        }
    }

    public static class CICache
    {
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

        public static List<string> GetOrAdd(string cacheItem)
        {
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(1);
            return cache.GetOrAdd(cacheItem, () => { Console.WriteLine("List initialized..."); return new List<string>(); }, policy);
        }
        public static List<string> Get(string cacheItem)
        {
            return cache.Get<List<string>>(cacheItem);
        }

    }


}
