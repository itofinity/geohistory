using Itofinity.Geohistory.Spi.Domain;
using UK.CO.Itofinity.GeoHistory.Spi.Core;

namespace UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication
{
    public interface IPublication : INamed
    {
        string Title { get;  }
        string Description { get; }
    }
}
