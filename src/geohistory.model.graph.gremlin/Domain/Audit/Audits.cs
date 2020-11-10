using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit
{
    public class Audits : Relationship
    {
        public Audits(string auditSessionId, string whatId) : base(auditSessionId, "audits", whatId, null, null, null, auditSessionId)
        {
        }
    }
}
