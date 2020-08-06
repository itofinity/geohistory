using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Cited : Relationship
    {
        public Cited(string citationId, string whatId, string auditSessionId) : base(citationId, "cited", whatId, null, auditSessionId)
        {
        }
    }
}
