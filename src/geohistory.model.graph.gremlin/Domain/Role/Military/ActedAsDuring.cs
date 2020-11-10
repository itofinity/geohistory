using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Role.Military
{
    public class ActedAsDuring : Relationship
    {
        public ActedAsDuring(string actorId, string roleId, string startDateTimeId, string endDateTimeId, string publicationId, int startPage, int endPage, string auditSessionId) : base(actorId, "actedas", roleId, new Dictionary<string, object>() { { "startDateTimeId", startDateTimeId }, { "endDateTimeId", endDateTimeId } }, publicationId, startPage, endPage, auditSessionId)
        { }

        public ActedAsDuring(string actorId, string roleId, string startDateTimeId, string publicationId, int startPage, int endPage, string auditSessionId) : base(actorId, "actedas", roleId, new Dictionary<string, object>() { { "startDateTimeId", startDateTimeId } }, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
