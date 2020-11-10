﻿using System;
using System.Collections.Generic;
using System.Web;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.People;
using static tinkerpop.scripts.ScriptBuilder;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.People
{
    public class Person : AbstractPropertyEntity, IPerson
    {
        public const string Label = "person";

        public Person(string name, string publicationId, string auditSessionId) : base(name, Label, new Dictionary<string, object>(), publicationId, auditSessionId)
        {
        }

        public Person(string name, Dictionary<string, object> properties, string publicationId, int startPage, int endPage, string auditSessionId) : base(name, Label, properties, publicationId, startPage, endPage, auditSessionId)
        {
        }

        public Person(string givenName, string familyName, List<char> initials, string email, string publicationId, int startPage, int endPage, string auditSessionId) 
            : base(BuildName(familyName, givenName, initials, email), Label, new Dictionary<string, object>() { { "givenName", givenName }, { "familyName", familyName }, { "initials", initials}, { "email", email } }, publicationId, startPage, endPage, auditSessionId)
        {
        }

        private static string BuildName(string familyName, string givenName, List<char> initials, string email)
        {
            return $"{familyName}, {givenName} {string.Join(",", initials)}, {email}";
        }

        public string GivenName { get { return (string)Properties["givenName"]; } }
        public string FamilyName { get { return (string)Properties["familyName"]; } }
        public List<char> Initials { get { return (List<char>)Properties["initials"]; } }
        public string Email { get { return (string)Properties["email"]; } }
    }
}