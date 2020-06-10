using Uk.Co.Itofinity.Geohistory.Model.Time;

namespace Uk.Co.Itofinity.Geohistory.Model.Role
{
    public class TemporalRole : AbstractTemporal
    {
        public TemporalRole(IRole role, IFuzzyDateTime startDateTime) : base(startDateTime, null) => this.Role = role;

        public IRole Role { get; }
    }
}