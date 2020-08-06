namespace Uk.Co.Itofinity.GeoHistory.Model.Location
{
    public interface ILocation : INamed
    {
        double? Latitude { get; }
        double? Longtitude { get; }

        void AddLatitudeLongtitude(double latitude, double longtitude);
    }
}
