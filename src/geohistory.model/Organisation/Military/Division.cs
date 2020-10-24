using System.Globalization;
using UK.CO.Itofinity.GeoHistory.Model.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Citation;
using UK.CO.Itofinity.GeoHistory.Model.Role;

namespace UK.CO.Itofinity.GeoHistory.Model.Organisation.Military
{
    public class Division : AbstractOrganisation
    {
        public const long DEFAULT_CONTROL_RANGE = 2000;
        public const long DEFAULT_INFLUENCE_RANGE = DEFAULT_CONTROL_RANGE * 2;

        public Division(string name, TemporalRole role, RegionInfo country, ICitation citation, IAudit audit) : base(name, role, country, DEFAULT_CONTROL_RANGE, DEFAULT_INFLUENCE_RANGE, citation, audit)
        {
        }

        public override string Size => Glossary.Division;
    }
}