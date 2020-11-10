using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Hosts : Relationship
    {
        public Hosts(string whereId, string whatId, string publicationId, int startPage, int endPage, string auditSessionId) : base(whereId, "hosts", whatId, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
