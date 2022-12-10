using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Publication
{
    public class Edited : Relationship
    {
        public Edited(string editorId, string publicationId, string citationId, string auditSessionId) : base(editorId, "edited", publicationId, citationId, auditSessionId)
        {
        }
    }
}
