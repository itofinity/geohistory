using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Audit
{
    public class Audits : Relationship
    {
        public Audits(string auditSessionId, string whatId) : base(auditSessionId, "audits", whatId, null, auditSessionId)
        {
        }
    }
}
