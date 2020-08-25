using geohistory.api.placeholder.Data;
using geohistory.spi.Data;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace geohistory.data.remote.google.books
{
    public class BookService
    {
        private readonly string apiKey;
        private ICache<object> cache = new WaitToFinishMemoryCache<object>();

        public BookService(string apikey)
        {
            this.apiKey = apikey;
        }

        public async System.Threading.Tasks.Task<Google.Apis.Books.v1.Data.Volumes> Query(string query)
        {
            var service = new BooksService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });
            var listreq = service.Volumes.List();
            listreq.Q = "mailed fist";
            return await listreq.ExecuteAsync();
        }
        
    }
}
