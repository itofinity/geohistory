using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Organisation
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
