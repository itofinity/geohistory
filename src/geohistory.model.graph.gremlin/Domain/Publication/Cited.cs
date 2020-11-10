using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Cited : Relationship
    {
        public Cited(string publicationId, int startPage, int endPage, string whatId, string auditSessionId) : base(publicationId, "cited", whatId, new Dictionary<string, object> { { "startpage", startPage + "" }, { "endpage", endPage + "" } }, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
