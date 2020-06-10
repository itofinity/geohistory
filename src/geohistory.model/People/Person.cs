using System;
using System.Collections.Generic;
using Uk.Co.Itofinity.Geohistory.Model.Role;

namespace Uk.Co.Itofinity.Geohistory.Model.People
{
    public class Person
    {
        public Person(string familyName, string givenName, DateTime dateOfBirth)
        {
            this.FamilyName = familyName;
            this.GiveNames = new List<string>() { givenName };
            this.DateOfBirth = dateOfBirth;
            this.Roles = new List<TemporalRole>();
        }

        public string FamilyName { get; }

        public DateTime DateOfBirth { get; }

        public string GivenName 
        {
            get 
            {
                return GiveNames[0];
            }
        }

        public List<string> GiveNames { get; }

        public void AddRole(TemporalRole temporalRole)
        {
            throw new NotImplementedException();
        }

        public List<TemporalRole> Roles { get; }

    }
}