using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Publication
{
    public class Cited : Relationship
    {
        public Cited(string citationId, string whatId, string auditSessionId) : base(citationId, "cited", whatId, null, auditSessionId)
        {
        }
    }
}
