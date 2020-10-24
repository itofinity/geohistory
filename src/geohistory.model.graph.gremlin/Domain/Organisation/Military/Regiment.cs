using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Organisation.Military
{
    public class Regiment : Unit
    {
        public Regiment(string title, string citationId, string auditSessionId) : base(title, "regiment", citationId, auditSessionId)
        {
        }
    }
}
