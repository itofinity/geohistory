using System;
using System.Collections.Generic;
using System.Web;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Organisation.Commercial;
using static tinkerpop.scripts.ScriptBuilder;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Organisation.Commercial
{
    public class Publisher : AbstractPropertyEntity, IPublisher
    {
        public Publisher(string name, string citationId, string auditSessionId) : base(name, "publisher", new Dictionary<string, object>(), citationId, auditSessionId)
        {
        }

        public Publisher(string name, Dictionary<string, object> properties, string citationId, string auditSessionId) : base(name, "publisher", properties, citationId, auditSessionId)
        {
        }
    }
}
