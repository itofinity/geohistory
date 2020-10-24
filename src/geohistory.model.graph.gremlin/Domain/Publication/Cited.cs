using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Cited : Relationship
    {
        public Cited(string citationId, string whatId, string auditSessionId) : base(citationId, "cited", whatId, null, auditSessionId)
        {
        }
    }
}
