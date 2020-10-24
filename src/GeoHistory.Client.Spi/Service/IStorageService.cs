using System.Collections.Generic;
using System.Threading.Tasks;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin;

namespace UK.CO.Itofinity.GeoHistory.Client.Spi.Service
{
    public interface IStorageService
    {
        Task StoreAsync(IQuery item);
        Task StoreAsync(IEnumerable<IQuery> items);
        Task DeleteAsync(string type, string id);
        Task ClearAsync();
        Task<object> ListByTypeAsync(string type);
    }
}
