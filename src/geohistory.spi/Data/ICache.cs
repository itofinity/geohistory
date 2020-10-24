using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UK.CO.Itofinity.GeoHistory.Spi.Data
{
    public interface ICache<TItem>
    {
        Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem);

        string GenerateKey(params object[] parts);
    }
}
