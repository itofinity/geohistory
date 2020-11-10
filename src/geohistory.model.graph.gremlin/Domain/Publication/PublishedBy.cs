using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class PublishedBy : Relationship
    {
        public PublishedBy(string whatId, string whoId, string publicationId, string auditSessionId) : base(whatId, "publishedby", whoId, publicationId, 0, 0, auditSessionId)
        {
        }
    }
}
