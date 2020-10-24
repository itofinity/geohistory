using System.Collections.Generic;
using System.Threading.Tasks;

namespace UK.CO.Itofinity.GeoHistory.Client.Spi.Service
{
    public interface ISearchService<T>
    {
        string Name { get; }

        Task<List<T>> Search(string term, int startIndex, int maxResult);
    }
}
