using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UK.CO.Itofinity.GeoHistory.Data.Books.Remote.Google
{
    public interface IBookService
    {
        Task<global::Google.Apis.Books.v1.Data.Volumes> Query(string query, int startIndex, int maxResults);
    }
}
