using System;
using System.Collections.Generic;
using System.Web;

using static tinkerpop.scripts.ScriptBuilder;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public abstract class AbstractLocation : AbstractPropertyEntity, ILocation
    {
        protected AbstractLocation(string name, string type, string publicationId, int startPage, int endPage, string auditSessionId) : base(name, type, new Dictionary<string, object>(), publicationId, startPage, endPage, auditSessionId)
        {
        }

        protected AbstractLocation(string name, string type, Dictionary<string, object> properties, string publicationId, int startPage, int endPage, string auditSessionId) : base(name, type, properties, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
