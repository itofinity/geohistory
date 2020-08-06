using System.Collections.Generic;
using System.Web;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core.Time;
using static tinkerpop.scripts.ScriptBuilder;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Time
{
    public class KeyDateTime : AbstractCitedAuditedEntity
    {
        public KeyDateTime(string name, string citationId, string auditSessionId) : base(name, "keydatetime", citationId, auditSessionId)
        {
        }
    }
}
