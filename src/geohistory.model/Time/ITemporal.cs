using UK.CO.Itofinity.GeoHistory.Model.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Citation;

namespace UK.CO.Itofinity.GeoHistory.Model.Time
{
    public interface ITemporal : INamed, ICited, IAudited
    {
        IFuzzyDateTime StartDateTime { get; }
        IFuzzyDateTime EndDateTime { get; }
        IFuzzyDateTimeRange DateTimeRange { get; }
    }
}