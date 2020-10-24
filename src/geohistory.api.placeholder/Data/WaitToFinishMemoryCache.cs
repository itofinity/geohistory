using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using UK.CO.Itofinity.GeoHistory.Spi.Data;

namespace UK.CO.Itofinity.GeoHistory.Api.Placeholder.Data
{
    /// <summary>
    /// <see cref="https://michaelscodingspot.com/cache-implementations-in-csharp-net/"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class WaitToFinishMemoryCache<TItem> : ICache<TItem>
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public string GenerateKey(params object[] parts)
        {
            Console.Out.WriteLine(string.Join("X", parts));
            return string.Join(string.Empty, parts.Select(p => p.ToString())).GetHashCode().ToString();  
        }

        public async Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem)
        {
            TItem cacheEntry;

            if (!_cache.TryGetValue(key, out cacheEntry))// Look for cache key.
            {
                SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();
                try
                {
                    if (!_cache.TryGetValue(key, out cacheEntry))
                    {
                        // Key not in cache, so get data.
                        cacheEntry = await createItem();
                        _cache.Set(key, cacheEntry);
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }
    }
}
