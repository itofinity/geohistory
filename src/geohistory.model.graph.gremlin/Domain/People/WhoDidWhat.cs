using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.People
{
    public class WhoDidWhat : Relationship
    {
        public WhoDidWhat(string whoId, string whatId, string publicationId, int startPage, int endPage, string auditSessionId) : base(whoId, "did", whatId, publicationId, startPage, endPage, auditSessionId)
        {
        }

        public WhoDidWhat(string whoId, string whatId, string auditSessionId) : base(whoId, "did", whatId, null, null, null, auditSessionId)
        {
        }
    }
}
