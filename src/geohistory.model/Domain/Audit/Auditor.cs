using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Audit
{
    public class Auditor : Person
    {
        public Auditor(string givenName, string familyName, List<char> initials, string email) : base(givenName, familyName, initials, email.Replace("@","_at_"))
        {
        }
    }
}
