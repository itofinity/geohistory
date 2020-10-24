﻿using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Organisation.Military
{
    public class Unit : AbstractPropertyEntity
    {
        public Unit(string title, string size, string citationId, string auditSessionId) : base(title, "unit", new Dictionary<string, object>() { { "size", size } }, citationId, auditSessionId)
        {
        }
    }
}
