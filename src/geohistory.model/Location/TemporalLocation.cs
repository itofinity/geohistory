using Uk.Co.Itofinity.GeoHistory.Model.Audit;
using Uk.Co.Itofinity.GeoHistory.Model.Citation;
using Uk.Co.Itofinity.GeoHistory.Model.Time;

namespace Uk.Co.Itofinity.GeoHistory.Model.Location
{
    public class TemporalLocation : AbstractTemporal
    {
        private SimplePostalAddress talbotArms;
        private FuzzyDateRange fuzzyDateRange;

        public TemporalLocation(ILocation location, IFuzzyDateTimeRange dateTimeRange, ICitation citation, IAudit audit) : base(dateTimeRange.StartDateTime, dateTimeRange.EndDateTime, citation, audit)
        {
            Location = location;
        }

        public override string Name => ToString();

        public override string ShortName => ToString();

        public ILocation Location { get; }

        public override string ToString()
        {
            return $"{Location.Name} {StartDateTime.DateTime.ToString(StartDateTime.Format)} {EndDateTime?.DateTime.ToString(EndDateTime?.Format)}";
        }
    }
}
