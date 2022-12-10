using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Core
{
    public class Relationship
    {
        public Relationship(string vertexOneId, string edge, string vertexTwoId, Dictionary<string, object> properties, string citationId, string auditSessionId)
        {
            VertexOneId = vertexOneId ?? throw new System.ArgumentNullException(nameof(vertexOneId));
            Edge = edge ?? throw new System.ArgumentNullException(nameof(edge));
            VertexTwoId = vertexTwoId ?? throw new System.ArgumentNullException(nameof(vertexTwoId));
            Properties = properties ?? throw new System.ArgumentNullException(nameof(properties));
            if (!"auditedby".Equals(edge.ToLower())
                && !"audits".Equals(edge.ToLower())
                && !"cited".Equals(edge.ToLower())
                )
            {
                CitationId = citationId ?? throw new System.ArgumentNullException(nameof(citationId));
            }
            AuditSessionId = auditSessionId ?? throw new System.ArgumentNullException(nameof(auditSessionId));
        }

        public Relationship(string vertexOneId, string edge, string vertexTwoId, string citationId, string auditSessionId) : this(vertexOneId, edge, vertexTwoId, new Dictionary<string, object>(), citationId, auditSessionId)
        {
        }

        public string VertexOneId { get; }
        public string Edge { get; }
        public string VertexTwoId { get; }
        public Dictionary<string, object> Properties { get; }
        public string CitationId { get; }
        public string AuditSessionId { get; }
    }
}
