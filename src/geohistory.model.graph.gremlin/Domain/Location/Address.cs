using System;
using System.Collections.Generic;
using System.Text;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Address : AbstractCitedAuditedEntity
    {
        public Address(string publicationId, int startPage, int endPage, string auditSessionId) : base(Guid.NewGuid().ToString(), "address", publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
