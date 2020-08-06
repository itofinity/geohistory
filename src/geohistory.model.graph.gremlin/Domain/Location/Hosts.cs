using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Hosts : Relationship
    {
        public Hosts(string whereId, string whatId, string citationId, string auditSessionId) : base(whereId, "hosts", whatId, citationId, auditSessionId)
        {
        }
    }
}
