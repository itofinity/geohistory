using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Time
{
    public class WhatHappenedWhen : Relationship
    {
        public WhatHappenedWhen(string whatId, string whenId, string citationId, string auditSessionId) : base(whatId, "happenedon", whenId, citationId, auditSessionId)
        {
        }
    }
}
