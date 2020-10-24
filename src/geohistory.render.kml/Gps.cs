using System;

namespace UK.CO.Itofinity.GeoHistory.Render.Kml
{
    public struct Gps
    {
        // In degrees
        public readonly double Latitude;
        public readonly double Longtitude;

        public Gps(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", Latitude, Longtitude);
        }

        public Vector ToUnitVector()
        {
            double lat = Latitude / 180 * Math.PI;
            double lng = Longtitude / 180 * Math.PI;

            // Z is North
            // X points at the Greenwich meridian
            return new Vector(Math.Cos(lng) * Math.Cos(lat), Math.Sin(lng) * Math.Cos(lat), Math.Sin(lat));
        }
    }
}
