using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.Geohistory.Model.Location
{
    public interface ILocation : INamed
    {
        double? Latitude { get; }
        double? Longtitude { get; }

        void AddLatitudeLongtitude(double latitude, double longtitude);
    }
}
