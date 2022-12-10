using System;
using System.Collections.Generic;
using System.Web;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain
{
    public class Person : INamed, IIdentifiable
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

        public string ShortName => GivenName; // TODO is this right?

        public List<char> Initials { get; }
        public string Email { get; }
    }
}
