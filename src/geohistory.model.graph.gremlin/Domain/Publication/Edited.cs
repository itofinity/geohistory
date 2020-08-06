using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Edited : Relationship
    {
        public Edited(string editorId, string publicationId, string citationId, string auditSessionId) : base(editorId, "edited", publicationId, citationId, auditSessionId)
        {
        }
    }
}
