using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Publication
{
    public class Published : Relationship
    {
        public Published(string publisherId, string publicationId, string citationId, string auditSessionId) : base(publisherId, "published", publicationId, citationId, auditSessionId)
        {
        }
    }
}
