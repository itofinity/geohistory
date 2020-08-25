using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace geohistory.spi.Data
{
    public interface ICache<TItem>
    {
        Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem);
    }
}
