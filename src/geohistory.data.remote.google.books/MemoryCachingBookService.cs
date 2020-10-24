using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Books.v1.Data;
using UK.CO.Itofinity.GeoHistory.Api.Placeholder.Data;
using UK.CO.Itofinity.GeoHistory.Spi.Data;

namespace UK.CO.Itofinity.GeoHistory.Data.Books.Remote.Google
{
    public class MemoryCachingBookService : IBookService
    {
        private ICache<Volumes> cache = new WaitToFinishMemoryCache<Volumes>();
        private IBookService underlyingBookService;

        public MemoryCachingBookService(IBookService underlyingBookService)
        {
            this.underlyingBookService = underlyingBookService ?? throw new ArgumentNullException(nameof(underlyingBookService));
        }

        public async Task<Volumes> Query(string query, int startIndex, int maxResults)
        {
            return await cache.GetOrCreate(cache.GenerateKey(query, startIndex, maxResults), () => { return underlyingBookService.Query(query, startIndex, maxResults); });
        }
    }
}
