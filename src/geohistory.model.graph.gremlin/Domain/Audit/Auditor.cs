using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.People;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit
{
    public class Auditor : Person
    {
        public Auditor(string name, string email, string auditSessionId) : base(name, email.Replace("@","_at_"), auditSessionId)
        {
        }
    }
}
