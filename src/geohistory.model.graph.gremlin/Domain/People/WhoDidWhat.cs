using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.People
{
    public class WhoDidWhat : Relationship
    {
        public WhoDidWhat(string whoId, string whatId, string citationId, string auditSessionId) : base(whoId, "did", whatId, citationId, auditSessionId)
        {
        }
    }
}
