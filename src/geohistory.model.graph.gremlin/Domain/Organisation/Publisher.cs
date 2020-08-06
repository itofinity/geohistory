using System;
using System.Collections.Generic;
using System.Web;

using static tinkerpop.scripts.ScriptBuilder;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Organisation
{
    public class Publisher : AbstractPropertyEntity
    {
        public Publisher(string name, string citationId, string auditSessionId) : base(name, "publisher", new Dictionary<string, object>(), citationId, auditSessionId)
        {
        }

        public Publisher(string name, Dictionary<string, object> properties, string citationId, string auditSessionId) : base(name, "publisher", properties, citationId, auditSessionId)
        {
        }
    }
}
