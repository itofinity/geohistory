using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Time
{
    public class WhenWhatHappened : Relationship
    {
        public WhenWhatHappened(string whenId, string whatId, string publicationId, int startPage, int endPage, string auditSessionId) : base(whenId, "dateof", whatId, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
