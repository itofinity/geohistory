using Uk.Co.Itofinity.Geohistory.Model.Audit;
using Uk.Co.Itofinity.Geohistory.Model.Citation;

namespace Uk.Co.Itofinity.Geohistory.Model.Time
{
    public interface ITemporal : INamed, ICited, IAudited
    {
        IFuzzyDateTime StartDateTime { get; }
        IFuzzyDateTime EndDateTime { get; }
        IFuzzyDateTimeRange DateTimeRange { get; }
    }
}