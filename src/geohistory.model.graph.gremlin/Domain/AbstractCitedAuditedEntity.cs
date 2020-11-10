using System;
using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain
{
    public abstract class AbstractCitedAuditedEntity : AbstractIdentifiableEntity
    {
        /// <summary>
        /// Special constructor for use by Publications, to Cite themselves
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="auditSessionId"></param>
        protected AbstractCitedAuditedEntity(string name, string type, string auditSessionId) : base(name, type)
        {
            PublicationId = this.Id;
            StartPage = 0;
            EndPage = 0;
            AuditSessionId = auditSessionId ?? throw new ArgumentNullException(nameof(auditSessionId));
        }

        /// <summary>
        /// Special constructor for use by Publishers, to Cite themselves
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="auditSessionId"></param>
        protected AbstractCitedAuditedEntity(string name, string type, string publicationId, string auditSessionId) : base(name, type)
        {
            PublicationId = publicationId ?? throw new ArgumentNullException(nameof(publicationId));
            StartPage = 0;
            EndPage = 0;
            AuditSessionId = auditSessionId ?? throw new ArgumentNullException(nameof(auditSessionId));
        }

        protected AbstractCitedAuditedEntity(string name, string type, string publicationId, int startPage, int endPage, string auditSessionId) : base(name, type)
        {
            PublicationId = publicationId ?? throw new ArgumentNullException(nameof(publicationId));
            StartPage = startPage;
            EndPage = endPage;
            AuditSessionId = auditSessionId ?? throw new ArgumentNullException(nameof(auditSessionId));
        }

        public string PublicationId { get; }
        public int StartPage { get; }
        public int EndPage { get; }
        public string AuditSessionId { get; }

        public override List<string> ToInsertQueries()
        {
            var entries = InitEntries();

            entries.Add(GetAddIdentifiableScript().Build());

            return entries;
        }

        protected List<string> InitEntries()
        {
            var entries = new List<string>();
            entries.AddRange(new Cited(PublicationId, StartPage, EndPage, Id, AuditSessionId).ToInsertQueries());
            entries.AddRange(new IsCited(Id, PublicationId, StartPage, EndPage, AuditSessionId).ToInsertQueries());
            entries.AddRange(new AuditedBy(Id, AuditSessionId).ToInsertQueries());
            entries.AddRange(new Audits(AuditSessionId, Id).ToInsertQueries());
            return entries;
        }
    }
}
