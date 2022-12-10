using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Publication
{
    public class HasCitation : Relationship
    {
        public HasCitation(string whatId, string citationId, string auditSessionId) : base(whatId, "hascitation", citationId, citationId, auditSessionId)
        {
        }
    }
}
