using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class IsCited : Relationship
    {
        public IsCited(string whatId, string publicationId, int startPage, int endPage, string auditSessionId) : base(whatId, "iscited", publicationId, new Dictionary<string, object> { { "startpage", startPage + "" }, { "endpage", endPage + "" } }, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
