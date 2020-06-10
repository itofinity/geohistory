using Uk.Co.Itofinity.Geohistory.Model.Role;

namespace Uk.Co.Itofinity.Geohistory.Model.Time
{
    public abstract class AbstractTemporal : ITemporal
    {
        protected AbstractTemporal(IFuzzyDateTime startDateTime, IFuzzyDateTime endDateTime)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

        public IFuzzyDateTime StartDateTime { get; }
        public IFuzzyDateTime EndDateTime { get; }
    }
}