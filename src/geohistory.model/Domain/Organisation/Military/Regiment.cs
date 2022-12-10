using Uk.Co.Itofinity.GeoHistory.Model.Domain.Audit;
using Uk.Co.Itofinity.GeoHistory.Model.Domain.Publication;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Organisation.Military
{
    public class Regiment : Unit
    {
        public Regiment(string title, string citationId, string auditSessionId) : base(title, "regiment", citationId, auditSessionId)
        {
        }
    }
}
