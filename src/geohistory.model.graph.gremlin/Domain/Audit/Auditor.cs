using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.People;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit
{
    public class Auditor : Person
    {
        public Auditor(string givenName, string familyName, List<char> initials, string email, string citationId, string auditSessionId) : base(givenName, familyName, initials, email.Replace("@","_at_"), citationId, auditSessionId)
        {
        }
    }
}
