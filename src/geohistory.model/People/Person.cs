using System;
using System.Collections.Generic;
using System.Globalization;
using Uk.Co.Itofinity.Geohistory.Model.Audit;
using Uk.Co.Itofinity.Geohistory.Model.Citation;
using Uk.Co.Itofinity.Geohistory.Model.Location;
using Uk.Co.Itofinity.Geohistory.Model.Organisation;
using Uk.Co.Itofinity.Geohistory.Model.Organisation.Military;
using Uk.Co.Itofinity.Geohistory.Model.Role;

namespace Uk.Co.Itofinity.Geohistory.Model.People
{
    public class Person : IEntity
    {
        public Person(string familyName, string givenName, DateTime? dateOfBirth, RegionInfo country, ICitation citation, IAudit audit)
        {
            this.FamilyName = familyName;
            this.GiveNames = new List<string>() { givenName };
            this.DateOfBirth = dateOfBirth;
            this.Appointments = new List<TemporalRole>();
            this.Country = country;
            Citation = citation;
            Audit = audit;
            this.Locations = new List<TemporalLocation>();
            this.Hierarchy = new HashSet<TemporalChainOfCommand>();
        }

        public string FamilyName { get; }

        public DateTime? DateOfBirth { get; }

        public string GivenName 
        {
            get 
            {
                return GiveNames[0];
            }
        }

        public List<string> GiveNames { get; }

        public List<TemporalRole> Appointments { get; }

        public HashSet<TemporalChainOfCommand> Hierarchy { get; }

        public IZone ZoneOfInfluence => new SimpleZone(1.0, 1.0);

        public IZone ZoneOfControl => new SimpleZone(1.0, 1.0);

        public RegionInfo Country { get;  }

        public string Name => ToString();

        public string ShortName => $"{FamilyName}";

        public string Size => Glossary.Person;

        public List<TemporalRole> Roles => Appointments;

        public ICitation Citation { get; }

        public IAudit Audit { get; }

        public List<TemporalLocation> Locations { get;  }
        public void AddLocation(TemporalLocation location)
        {
            Locations.Add(location);
        }

        public override string ToString()
        {
            return $"{FamilyName} {GivenName}";
        }

        public void AddAppointment(TemporalRole commandFirstDerbyshireYeomanry)
        {
            Appointments.Add(commandFirstDerbyshireYeomanry);
        }
    }
}