using System.Collections.Generic;
using System.Globalization;
using Uk.Co.Itofinity.GeoHistory.Model.Audit;
using Uk.Co.Itofinity.GeoHistory.Model.Citation;
using Uk.Co.Itofinity.GeoHistory.Model.Location;
using Uk.Co.Itofinity.GeoHistory.Model.Organisation;
using Uk.Co.Itofinity.GeoHistory.Model.Role;

namespace Uk.Co.Itofinity.GeoHistory.Model
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
