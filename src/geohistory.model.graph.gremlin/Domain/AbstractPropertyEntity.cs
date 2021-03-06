﻿using System;
using System.Collections.Generic;
using System.Web;
using tinkerpop.scripts;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain
{
    public abstract class AbstractPropertyEntity : AbstractCitedAuditedEntity
    {
        protected AbstractPropertyEntity(string name, string type, Dictionary<string, object> properties, string citationId, string auditSessionId) : base(name, type, citationId, auditSessionId)
        {
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public Dictionary<string, object> Properties { get; }

        public override List<string> ToInsertQueries()
        {
            var entries = InitEntries();
            ScriptBuilder script = GetPropertiesScript();
            entries.Add(script.Build());
            return entries;
        }

        protected ScriptBuilder GetPropertiesScript()
        {
            var script = GetIdentifiableScript();
            foreach (var entry in Properties)
            {
                script.property(entry.Key, entry.Value);
            }

            return script;
        }
    }
}
