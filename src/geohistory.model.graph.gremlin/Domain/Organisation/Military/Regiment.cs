using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Organisation.Military
{
    public class Regiment : Unit
    {
        public Regiment(string title, string publicationId, int startPage, int endPage, string auditSessionId) : base(title, "regiment", publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
