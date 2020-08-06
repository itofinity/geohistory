using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Published : Relationship
    {
        public Published(string publisherId, string publicationId, string citationId, string auditSessionId) : base(publisherId, "published", publicationId, citationId, auditSessionId)
        {
        }
    }
}
