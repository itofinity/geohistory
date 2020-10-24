namespace UK.CO.Itofinity.GeoHistory.Render.Kml
{
    internal class Metadata
    {
        public Metadata(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        public double Latitude { get; }
        public double Longitude { get; }
    }
}
