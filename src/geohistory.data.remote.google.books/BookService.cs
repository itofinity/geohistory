using Google.Apis.Books.v1;
using Google.Apis.Services;
using UK.CO.Itofinity.GeoHistory.Spi.Data;

namespace UK.CO.Itofinity.GeoHistory.Data.Books.Remote.Google
{
    public class BookService : IBookService
    {
        private readonly string apiKey;

        public BookService(string apikey)
        {
            this.apiKey = apikey;
        }

        public async System.Threading.Tasks.Task<global::Google.Apis.Books.v1.Data.Volumes> Query(string query, int startIndex, int maxResults)
        {
            var service = new BooksService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });
            var listreq = service.Volumes.List();
            listreq.Q = query;
            listreq.MaxResults = maxResults;
            listreq.StartIndex = startIndex;
            return await listreq.ExecuteAsync();
        }
        
    }
}
