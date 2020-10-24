using UK.CO.Itofinity.GeoHistory.Model.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Citation;
using UK.CO.Itofinity.GeoHistory.Model.Time;

namespace UK.CO.Itofinity.GeoHistory.Model.Location
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
