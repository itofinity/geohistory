using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Publication
{
    public class PublishedBy : Relationship
    {
        public PublishedBy(string whatId, string whoId, string citationId, string auditSessionId) : base(whatId, "publishedby", whoId, citationId, auditSessionId)
        {
        }
    }
}
