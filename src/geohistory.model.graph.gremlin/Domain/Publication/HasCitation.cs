using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class HasCitation : Relationship
    {
        public HasCitation(string whatId, string citationId, string auditSessionId) : base(whatId, "hascitation", citationId, citationId, auditSessionId)
        {
        }
    }
}
