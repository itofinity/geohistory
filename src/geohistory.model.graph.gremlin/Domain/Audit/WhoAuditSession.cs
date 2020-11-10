using System;
using System.Collections.Generic;
using System.Text;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.People;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit
{
    public class WhoAuditSession : WhoDidWhat
    {
        public WhoAuditSession(string whoId, string auditSessionId) : base(whoId, auditSessionId, auditSessionId)
        {
        }
    }
}
