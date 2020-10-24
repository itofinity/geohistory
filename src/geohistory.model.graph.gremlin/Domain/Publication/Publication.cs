using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Spi.Core;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication
{
    public class Publication : AbstractPropertyEntity, IPublication
    {
        public const string Label = "publication";

        public Publication(string title, string citationId, string auditSessionId) : base(title, Label, new Dictionary<string, object>(), citationId, auditSessionId)
        {
        }

        public string Title { get; }

        public string Description { get; }
    }
}
