using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class PublishedBy : Relationship
    {
        public PublishedBy(string whatId, string whoId, string citationId, string auditSessionId) : base(whatId, "publishedby", whoId, citationId, auditSessionId)
        {
        }
    }
}
