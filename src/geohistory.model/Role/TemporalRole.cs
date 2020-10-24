using UK.CO.Itofinity.GeoHistory.Model.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Citation;
using UK.CO.Itofinity.GeoHistory.Model.Time;

namespace UK.CO.Itofinity.GeoHistory.Model.Role
{
    public class TemporalRole : AbstractTemporal
    {
        public TemporalRole(IRole role, IFuzzyDateTimeRange dateRange, ICitation citation, IAudit audit) : base(dateRange.StartDateTime, dateRange.EndDateTime, citation, audit) => this.Role = role;

        public IRole Role { get; }

        public override string Name => ToString();

        public override string ShortName => $"{Role.ShortName}/{StartDateTime.DateTime.ToString(StartDateTime.Format)}";

        public override string ToString()
        {
            return $"{Role.Name} {StartDateTime.DateTime.ToString(StartDateTime.Format)} {EndDateTime?.DateTime.ToString(EndDateTime?.Format)}";
        }
    }
}