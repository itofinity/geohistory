using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;
using static tinkerpop.scripts.ScriptBuilder;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core
{
    public class Relationship : IQuery
    {
        public Relationship(string vertexOneId, string edge, string vertexTwoId, Dictionary<string, object> properties, string publicationId, int? startPage, int? endPage, string auditSessionId)
        {
            VertexOneId = vertexOneId ?? throw new System.ArgumentNullException(nameof(vertexOneId));
            Edge = edge ?? throw new System.ArgumentNullException(nameof(edge));
            VertexTwoId = vertexTwoId ?? throw new System.ArgumentNullException(nameof(vertexTwoId));
            Properties = properties ?? throw new System.ArgumentNullException(nameof(properties));
            PublicationId = publicationId;
            StartPage = startPage;
            EndPage = endPage;
            AuditSessionId = auditSessionId ?? throw new System.ArgumentNullException(nameof(auditSessionId));
        }

        public Relationship(string vertexOneId, string edge, string vertexTwoId, string publicationId, int? startPage, int? endPage, string auditSessionId) : 
            this(vertexOneId, edge, vertexTwoId, new Dictionary<string, object>(), publicationId, startPage, endPage, auditSessionId)
        {
        }

        public string VertexOneId { get; }
        public string Edge { get; }
        public string VertexTwoId { get; }
        public Dictionary<string, object> Properties { get; }
        public string AuditSessionId { get; }
        public string PublicationId { get; }
        public int? StartPage { get; }
        public int? EndPage { get; }

        public List<string> ToInsertQueries()
        {
            var queries = new List<string>();
            var script = g.V(VertexOneId).addE(Edge).to(g.V(VertexTwoId).Build());

            foreach (var entry in Properties)
            {
                script.property(entry.Key, entry.Value);
            }

            if (!string.IsNullOrWhiteSpace(PublicationId))
            {
                script.property("publicationId", PublicationId);
            }
            if (StartPage.HasValue)
            {
                script.property("startPage", StartPage);
            }
            if (EndPage.HasValue)
            {
                script.property("endPage", EndPage);
            }

            script.property("auditSessionId", AuditSessionId);
            queries.Add(script.Build());
            return queries;
        }

        public string ToFindQuery()
        {
            throw new System.NotImplementedException();
        }
    }
}
