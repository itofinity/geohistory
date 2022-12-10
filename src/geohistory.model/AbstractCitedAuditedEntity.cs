using System;

namespace Uk.Co.Itofinity.GeoHistory.Model
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
    }
}
