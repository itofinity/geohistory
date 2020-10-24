using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Wrote : Relationship
    {
        public Wrote(string authorId, string publicationId, string citationId, string auditSessionId) : base(authorId, "wrote", publicationId, citationId, auditSessionId)
        {
        }
    }
}
