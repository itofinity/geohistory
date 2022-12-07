using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model;
using Uk.Co.Itofinity.GeoHistory.Model.Location;

namespace Uk.Co.Itofinity.GeoHistory.Data.Remote.Google.Maps
{
    public interface IGeoLocationService
    {
        LatitudeLongtitude GetLatitudeLongtitude(ILocation location);
        LatitudeLongtitude GetLatitudeLongtitude(Entry entry);
        IEnumerable<LatitudeLongtitude> GetRoute(Entry from, Entry to);
    }
}