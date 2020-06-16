using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Uk.Co.Itofinity.Geohistory.Model.Audit;
using Uk.Co.Itofinity.Geohistory.Model.Citation;
using Uk.Co.Itofinity.Geohistory.Model.Location;
using Uk.Co.Itofinity.Geohistory.Model.Organisation;
using Uk.Co.Itofinity.Geohistory.Model.Role;

namespace Uk.Co.Itofinity.Geohistory.Model
{
    public interface IEntity : INamed, ICited, IAudited
    {
        HashSet<TemporalChainOfCommand> Hierarchy { get; }

        List<TemporalRole> Roles { get; }

        IZone ZoneOfInfluence { get; }
        IZone ZoneOfControl { get; }

        RegionInfo Country { get; }
        string Size { get; }

        List<TemporalLocation> Locations { get; }
    }
}
