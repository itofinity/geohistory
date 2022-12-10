﻿using Uk.Co.Itofinity.GeoHistory.Model.Core;

namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Publication
{
    public class Wrote : Relationship
    {
        public Wrote(string authorId, string publicationId, string citationId, string auditSessionId) : base(authorId, "wrote", publicationId, citationId, auditSessionId)
        {
        }
    }
}
