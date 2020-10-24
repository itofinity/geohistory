using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UK.CO.Itofinity.GeoHistory.Model.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Citation;
using UK.CO.Itofinity.GeoHistory.Model.Location;
using UK.CO.Itofinity.GeoHistory.Model.Role;

namespace UK.CO.Itofinity.GeoHistory.Model.Organisation.Military
{
    public abstract class AbstractOrganisation : IOrganisation
    {
        protected AbstractOrganisation(RegionInfo country) : this(null, null, country, 0, 0, null, null)
        {
        }

        protected AbstractOrganisation(string name, TemporalRole trole, RegionInfo country, long controlRange, long influenceRange, ICitation citation, IAudit audit)
        {
            this.Country = country;
            Citation = citation;
            Audit = audit;
            this.Name = $"{name} {Size}";
            this.Purposes = new List<TemporalRole>() { trole };

            this.ZoneOfControl = new SimpleZone(controlRange * trole.Role.ControlFactor, 1.0 / trole.Role.ControlFactor);
            this.ZoneOfInfluence = new SimpleZone(influenceRange * trole.Role.InfluenceFactor, 1.0 / trole.Role.InfluenceFactor);

            this.Personel = new List<TemporalRole>();
            this.Locations = new List<TemporalLocation>();
            this.Hierarchy = new HashSet<TemporalChainOfCommand>();
        }

        public RegionInfo Country { get; }
        public ICitation Citation { get; }
        public IAudit Audit { get; }
        public string Name { get; }

        public string ShortName
        {
            get
            {
                var parts = this.Name.Split(' ');
                var buffer = new StringBuilder();
                parts.ToList().ForEach(p => buffer.Append(p[0] + " "));
                return buffer.ToString().Trim();
            }
        }

        public void AddHierarchy(TemporalChainOfCommand temporalChainOfCommand)
        {
            Hierarchy.Add(temporalChainOfCommand);
        }

        public void AddPersonel(TemporalRole appointment)
        {
            Personel.Add(appointment);
        }

        public void AddPurpose(TemporalRole purpose)
        {
            Purposes.Add(purpose);
        }

        public void AddLocation(TemporalLocation location)
        {
            Locations.Add(location);
        }

        public HashSet<TemporalChainOfCommand> Hierarchy { get; }
        public abstract string Size { get; }
        public IZone ZoneOfControl { get; }
        public IZone ZoneOfInfluence { get; }

        public List<TemporalRole> Roles => Purposes;

        public List<TemporalRole> Purposes { get; }

        public List<TemporalRole> Personel { get; }

        public List<TemporalLocation> Locations { get; }
    }
}