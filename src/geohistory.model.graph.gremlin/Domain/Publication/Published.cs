using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Published : Relationship
    {
        public Published(string publisherId, string publicationId, string auditSessionId) : base(publisherId, "published", publicationId, publicationId, 0, 0, auditSessionId)
        {
        }
    }
}
