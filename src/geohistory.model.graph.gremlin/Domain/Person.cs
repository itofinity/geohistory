using System;
using System.Collections.Generic;
using System.Web;
using static tinkerpop.scripts.ScriptBuilder;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain
{
    public class Person : IQuery, INamed, IIdentifiable
    {
        public Person(string name) : this(name, name, new List<char>(), name)
        { }

        public Person(string givenName, string familyName, List<char> initials, string email)
        {
            GivenName = givenName ?? throw new ArgumentNullException(nameof(givenName));
            FamilyName = familyName ?? throw new ArgumentNullException(nameof(familyName));
            Initials = initials ?? throw new ArgumentNullException(nameof(initials));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public string Name => $"{FamilyName}, {GivenName} {string.Join(",", Initials)}, {Email}";

        public string Id => HttpUtility.UrlEncode(Name.Replace(",", "_"));

        public string GivenName { get; }
        public string FamilyName { get; }
        public List<char> Initials { get; }
        public string Email { get; }

        public List<string> ToInsertQueries()
        {
            var entries = new List<string>();
            entries.Add(g.AddV("person").property("id", Id).property("name", Name).property("pk", "pk").Build());
            return entries;
        }
    }
}
