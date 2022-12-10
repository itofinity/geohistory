using System;
using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Location
{
    public abstract class AbstractLocation : AbstractPropertyEntity, ILocation
    {
        protected AbstractLocation(string name, string type, string citationId, string auditSessionId) : base(name, type, new Dictionary<string, object>(), citationId, auditSessionId)
        {
        }

        protected AbstractLocation(string name, string type, Dictionary<string, object> properties, string citationId, string auditSessionId) : base(name, type, properties, citationId, auditSessionId)
        {
        }
    }
}
