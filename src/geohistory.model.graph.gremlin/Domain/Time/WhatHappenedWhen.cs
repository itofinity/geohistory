using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Time
{
    public class WhatHappenedWhen : Relationship
    {
        public WhatHappenedWhen(string whatId, string whenId, string publicationId, int startPage, int endPage, string auditSessionId) : base(whatId, "happenedon", whenId, publicationId, startPage, endPage, auditSessionId)
        {
        }

        public WhatHappenedWhen(string whatId, string whenId, string auditSessionId) : base(whatId, "happenedon", whenId, null, null, null, auditSessionId)
        {
        }
    }
}
