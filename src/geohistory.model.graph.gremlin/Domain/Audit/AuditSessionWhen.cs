using System;
using System.Collections.Generic;
using System.Text;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Time;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit
{
    public class AuditSessionWhen : WhatHappenedWhen
    {
        public AuditSessionWhen(string auditSessionId, string whenId) : base(auditSessionId, whenId, auditSessionId)
        {
        }
    }
}
