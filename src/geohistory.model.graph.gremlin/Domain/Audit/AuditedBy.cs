using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit
{
    public class AuditedBy : Relationship
    {
        public AuditedBy(string whatId, string sessionId) : base(whatId, "auditedby", sessionId, null, sessionId)
        {
        }
    }
}
