using geohistory.client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace geohistory.client.Services
{
    public class DataStoreSearchService : ISearchService
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public async Task<List<string>> Query(string query)
        {
            var items = await DataStore.GetItemsAsync();
            return items.Where(i => i.Text.Contains(query)).Select(i => i.Text).ToList();
        }
    }
}
