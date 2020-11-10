using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Wrote : Relationship
    {
        public Wrote(string authorId, string publicationId, string auditSessionId) : base(authorId, "wrote", publicationId, publicationId, 0, 0, auditSessionId)
        {
        }
    }
}
