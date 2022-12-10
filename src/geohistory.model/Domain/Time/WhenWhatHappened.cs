using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Time
{
    public class WhenWhatHappened : Relationship
    {
        public WhenWhatHappened(string whenId, string whatId, string citationId, string auditSessionId) : base(whenId, "dateof", whatId, citationId, auditSessionId)
        {
        }
    }
}
