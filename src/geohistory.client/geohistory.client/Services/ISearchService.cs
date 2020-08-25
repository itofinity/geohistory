using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace geohistory.client.Services
{
    public interface ISearchService
    {
        Task<List<string>> Query(string query);
    }
}
