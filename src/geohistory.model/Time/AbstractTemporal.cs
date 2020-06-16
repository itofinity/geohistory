using Uk.Co.Itofinity.Geohistory.Model.Audit;
using Uk.Co.Itofinity.Geohistory.Model.Citation;
using Uk.Co.Itofinity.Geohistory.Model.Role;

namespace Uk.Co.Itofinity.Geohistory.Model.Time
{
    public abstract class AbstractTemporal : ITemporal
    {
        protected AbstractTemporal(IFuzzyDateTime startDateTime, IFuzzyDateTime endDateTime, ICitation citation, IAudit audit)
        {
            DateTimeRange = new FuzzyDateRange(startDateTime, endDateTime);
            Citation = citation;
            Audit = audit;
        }

        public IFuzzyDateTime StartDateTime => DateTimeRange.StartDateTime;
        public IFuzzyDateTime EndDateTime => DateTimeRange.EndDateTime;

        public IFuzzyDateTimeRange DateTimeRange { get; }

        public abstract string Name { get; }

        public abstract string ShortName { get; }

        public ICitation Citation { get; }

        public IAudit Audit { get; }
    }
}