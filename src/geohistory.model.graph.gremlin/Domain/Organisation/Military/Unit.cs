using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Organisation.Military
{
    public class Unit : AbstractPropertyEntity
    {
        public Unit(string title, string size, string citationId, string auditSessionId) : base(title, "unit", new Dictionary<string, object>() { { "size", size } }, citationId, auditSessionId)
        {
        }
    }
}
