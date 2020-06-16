using Uk.Co.Itofinity.Geohistory.Model.Audit;
using Uk.Co.Itofinity.Geohistory.Model.Citation;
using Uk.Co.Itofinity.Geohistory.Model.Time;

namespace Uk.Co.Itofinity.Geohistory.Model.Role
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