using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Publication
{
    public class Authored : Relationship
    {
        public Authored(string whoId, string whatId, string citationId, string auditSessionId) : base(whoId, "authored", whatId, citationId, auditSessionId)
        {
        }
    }
}
