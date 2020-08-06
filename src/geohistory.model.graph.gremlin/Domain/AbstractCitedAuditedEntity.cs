using System;
using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain
{
    public abstract class AbstractCitedAuditedEntity : AbstractIdentifiableEntity
    {
        protected AbstractCitedAuditedEntity(string name, string type, string citationId, string auditSessionId) : base(name, type)
        {
            if(!"citation".Equals(type.ToLower()))
            {
                CitationId = citationId ?? throw new ArgumentNullException(nameof(citationId));
            }
            else
            {
                CitationId = Id;
            }
            
            AuditSessionId = auditSessionId ?? throw new ArgumentNullException(nameof(auditSessionId));
        }

        public string CitationId { get; }
        public string AuditSessionId { get; }

        public override List<string> ToInsertQueries()
        {
            var entries = InitEntries();

            entries.Add(GetIdentifiableScript().Build());

            return entries;
        }

        protected List<string> InitEntries()
        {
            var entries = new List<string>();
            if (!String.IsNullOrWhiteSpace(CitationId))
            {
                // catches the special case of Citations themselves being a AbstractCitedAuditedEntity
                entries.AddRange(new Cited(CitationId, Id, AuditSessionId).ToInsertQueries());
                entries.AddRange(new HasCitation(Id, CitationId, AuditSessionId).ToInsertQueries());
            }

            entries.AddRange(new AuditedBy(Id, AuditSessionId).ToInsertQueries());
            entries.AddRange(new Audits(AuditSessionId, Id).ToInsertQueries());
            return entries;
        }
    }
}
