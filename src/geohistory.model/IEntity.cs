using System.Collections.Generic;
using System.Globalization;
using UK.CO.Itofinity.GeoHistory.Model.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Citation;
using UK.CO.Itofinity.GeoHistory.Model.Location;
using UK.CO.Itofinity.GeoHistory.Model.Organisation;
using UK.CO.Itofinity.GeoHistory.Model.Role;

namespace UK.CO.Itofinity.GeoHistory.Model
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
