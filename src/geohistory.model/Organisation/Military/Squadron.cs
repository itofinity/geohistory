using System;
using System.Globalization;
using Uk.Co.Itofinity.Geohistory.Model.Audit;
using Uk.Co.Itofinity.Geohistory.Model.Citation;
using Uk.Co.Itofinity.Geohistory.Model.Location;
using Uk.Co.Itofinity.Geohistory.Model.Role;

namespace Uk.Co.Itofinity.Geohistory.Model.Organisation.Military
{
    public class Squadron : AbstractOrganisation
    {
        public const long DEFAULT_CONTROL_RANGE = 500;
        public const long DEFAULT_INFLUENCE_RANGE = DEFAULT_CONTROL_RANGE * 2;

        public Squadron(string name, TemporalRole role, RegionInfo country, ICitation citation, IAudit audit) : base(name, role, country, DEFAULT_CONTROL_RANGE, DEFAULT_INFLUENCE_RANGE, citation, audit)
        {
        }

        public override string Size => Glossary.Squadron;
    }
}