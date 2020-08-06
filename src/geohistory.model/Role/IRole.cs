namespace Uk.Co.Itofinity.GeoHistory.Model.Role
{
    public interface IRole : INamed
    {
        double ControlFactor { get; }

        double InfluenceFactor { get; }
    }
}