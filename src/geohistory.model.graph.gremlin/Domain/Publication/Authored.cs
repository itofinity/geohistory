using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Authored : Relationship
    {
        public Authored(string whoId, string whatId, string citationId, string auditSessionId) : base(whoId, "authored", whatId, citationId, auditSessionId)
        {
        }
    }
}
