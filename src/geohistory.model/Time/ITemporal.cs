using Uk.Co.Itofinity.GeoHistory.Model.Audit;
using Uk.Co.Itofinity.GeoHistory.Model.Citation;

namespace Uk.Co.Itofinity.GeoHistory.Model.Time
{
    public interface ITemporal : INamed, ICited, IAudited
    {
        IFuzzyDateTime StartDateTime { get; }
        IFuzzyDateTime EndDateTime { get; }
        IFuzzyDateTimeRange DateTimeRange { get; }
    }
}