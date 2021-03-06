﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Address : AbstractCitedAuditedEntity
    {
        public Address(string citationId, string auditSessionId) : base(Guid.NewGuid().ToString(), "address", citationId, auditSessionId)
        {
        }
    }
}
