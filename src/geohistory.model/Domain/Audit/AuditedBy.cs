using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Audit
{
    public class AuditedBy : Relationship
    {
        public AuditedBy(string whatId, string sessionId) : base(whatId, "auditedby", sessionId, null, sessionId)
        {
        }
    }
}
