using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Role.Military
{
    public class ActedAsDuring : Relationship
    {
        public ActedAsDuring(string actorId, string roleId, string startDateTimeId, string endDateTimeId, string citationId, string auditSessionId) : base(actorId, "actedas", roleId, new Dictionary<string, object>() { { "startDateTimeId", startDateTimeId }, { "endDateTimeId", endDateTimeId } }, citationId, auditSessionId)
        { }

        public ActedAsDuring(string actorId, string roleId, string startDateTimeId, string citationId, string auditSessionId) : base(actorId, "actedas", roleId, new Dictionary<string, object>() { { "startDateTimeId", startDateTimeId } }, citationId, auditSessionId)
        {
        }
    }
}
