namespace Uk.Co.Itofinity.Geohistory.Model.Role
{
    public interface IRole : INamed
    {
        double ControlFactor { get;  }

        double InfluenceFactor { get; }
    }
}