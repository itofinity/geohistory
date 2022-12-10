using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Time
{
    public class WhatHappenedWhen : Relationship
    {
        public WhatHappenedWhen(string whatId, string whenId, string citationId, string auditSessionId) : base(whatId, "happenedon", whenId, citationId, auditSessionId)
        {
        }
    }
}
