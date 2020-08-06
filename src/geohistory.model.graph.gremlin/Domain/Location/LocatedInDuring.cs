using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Role.Military
{
    public class LocatedInDuring : Relationship
    {
        public LocatedInDuring(string whatId, string whereId, string startDateTimeId, string endDateTimeId, string citationId, string auditSessionId) : base(whatId, "locatedin", whereId, new Dictionary<string, object>() { { "startDateTimeId", startDateTimeId }, { "endDateTimeId", endDateTimeId } }, citationId, auditSessionId)
        { }

        public LocatedInDuring(string whatId, string whereId, string startDateTimeId, string citationId, string auditSessionId) : base(whatId, "locatedin", whereId, new Dictionary<string, object>() { { "startDateTimeId", startDateTimeId } }, citationId, auditSessionId)
        {
        }
    }
}
