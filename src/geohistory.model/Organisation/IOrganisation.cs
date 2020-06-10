using System.Collections.Generic;
using Uk.Co.Itofinity.Geohistory.Model.Organisation;

namespace Uk.Co.Itofinity.Geohistory.Model
{
    public interface IOrganisation
    {
        HashSet<TemporalChainOfCommand> Superiors { get; }
        HashSet<TemporalChainOfCommand> Subbordinates { get; }
        string Size { get; }

        string Purpose { get; }

        string Name { get; }
    }
}