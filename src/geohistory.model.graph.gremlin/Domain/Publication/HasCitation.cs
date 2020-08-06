using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class HasCitation : Relationship
    {
        public HasCitation(string whatId, string citationId, string auditSessionId) : base(whatId, "hascitation", citationId, citationId, auditSessionId)
        {
        }
    }
}
