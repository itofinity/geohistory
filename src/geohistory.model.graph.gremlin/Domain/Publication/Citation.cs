using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Citation : AbstractPropertyEntity
    {
        public Citation(string publicationName, string auditSessionId) : base($"{publicationName}", "citation", new Dictionary<string, object>(), null, auditSessionId)
        {

        }
        public Citation(string publicationName, int startPage, string auditSessionId) : base($"{publicationName}_{startPage}", "citation", new Dictionary<string, object> { { "startpage", startPage + "" } }, null, auditSessionId)
        {
        }

        public Citation(string publicationName, int startPage, int endPage, string auditSessionId) : base($"{publicationName}_{startPage}-{endPage}", "citation", new Dictionary<string, object> { { "startpage", startPage + "" }, { "endpage", endPage + "" } }, null, auditSessionId)
        {
        }
    }
}
