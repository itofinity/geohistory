using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Role.Military
{
    public class LocatedInDuring : Relationship
    {
        public LocatedInDuring(string whatId, string whereId, string startDateTimeId, string endDateTimeId, string publicationId, int startPage, int endPage, string auditSessionId) : base(whatId, "locatedin", whereId, new Dictionary<string, object>() { { "startDateTimeId", startDateTimeId }, { "endDateTimeId", endDateTimeId } }, publicationId, startPage, endPage, auditSessionId)
        { }

        public LocatedInDuring(string whatId, string whereId, string startDateTimeId, string publicationId, int startPage, int endPage, string auditSessionId) : base(whatId, "locatedin", whereId, new Dictionary<string, object>() { { "startDateTimeId", startDateTimeId } }, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
