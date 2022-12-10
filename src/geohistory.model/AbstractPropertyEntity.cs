using System;
using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain
{
    public abstract class AbstractPropertyEntity : AbstractCitedAuditedEntity
    {
        protected AbstractPropertyEntity(string name, string type, Dictionary<string, object> properties, string citationId, string auditSessionId) : base(name, type, citationId, auditSessionId)
        {
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public Dictionary<string, object> Properties { get; }
    }
}
