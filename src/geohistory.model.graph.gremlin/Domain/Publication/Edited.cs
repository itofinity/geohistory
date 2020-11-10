using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Edited : Relationship
    {
        public Edited(string editorId, string publicationId, string auditSessionId) : base(editorId, "edited", publicationId, publicationId, 0, 0, auditSessionId)
        {
        }
    }
}
