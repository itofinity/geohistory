using System.Collections.Generic;
using System.Globalization;

namespace Uk.Co.Itofinity.Geohistory.Model.Organisation.Military
{
    public abstract class AbstractOrganisation : IOrganisation
    {
        protected AbstractOrganisation(RegionInfo country) : this(null, null, country)
        {
        }

        protected AbstractOrganisation(string name, string purpose, RegionInfo country)
        {
            this.Country = country;
            this.Name = name;
            this.Purpose = purpose;
        }

        public RegionInfo Country { get; }
        public string Name { get; }

        public string Purpose { get; }

        public void AddSuperior(TemporalChainOfCommand temporalChainOfCommand)
        {
            Superiors.Add(temporalChainOfCommand);
        }

        public void AddSubordinate(TemporalChainOfCommand temporalChainOfCommand)
        {
           Subbordinates.Add(temporalChainOfCommand);
        }

        public HashSet<TemporalChainOfCommand> Superiors { get; }
        public HashSet<TemporalChainOfCommand> Subbordinates { get; }
        public abstract string Size { get; }
    }
}