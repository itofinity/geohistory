using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Time
{
    public class WhenWhatHappened : Relationship
    {
        public WhenWhatHappened(string whenId, string whatId, string citationId, string auditSessionId) : base(whenId, "dateof", whatId, citationId, auditSessionId)
        {
        }
    }
}
