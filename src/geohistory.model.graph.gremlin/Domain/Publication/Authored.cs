using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Authored : Relationship
    {
        public Authored(string whoId, string whatId, string publicationId, int startPage, int endPage, string auditSessionId) : base(whoId, "authored", whatId, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
