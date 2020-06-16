using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.Geohistory.Model.Location
{
    public class LatitudeLongtitude
    {
        public LatitudeLongtitude()
        {
        }

        public LatitudeLongtitude(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }

        public double Latitude { get; }
        public double Longtitude { get; }

        public string Name => ToString();

        public string ShortName => ToString();

        public override string ToString()
        {
            return $"{Latitude},{Longtitude}";
        }
    }
}
