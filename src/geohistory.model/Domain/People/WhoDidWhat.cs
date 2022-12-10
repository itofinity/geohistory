using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.People
{
    public class WhoDidWhat : Relationship
    {
        public WhoDidWhat(string whoId, string whatId, string citationId, string auditSessionId) : base(whoId, "did", whatId, citationId, auditSessionId)
        {
        }
    }
}
