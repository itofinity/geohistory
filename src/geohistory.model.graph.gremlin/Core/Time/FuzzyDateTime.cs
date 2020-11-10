using System;
using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain;
using UK.CO.Itofinity.GeoHistory.Spi.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core.Time
{
    public class FuzzyDateTime : AbstractPropertyEntity, IFuzzyDateTime
    {
        public const string Label = "datetime";

        public FuzzyDateTime(DateTime dateTime, string auditSessionId) : this(dateTime, "F", null, 0, 0, auditSessionId)
        {
            // TODO is this needed ?
        }

        public FuzzyDateTime(DateTime dateTime, string publicationId, int startPage, int endPage, string auditSessionId) : this(dateTime, "F", publicationId, startPage, endPage, auditSessionId)
        {
        }

        public FuzzyDateTime(DateTime dateTime, string format, string publicationId, int startPage, int endPage, string auditSessionId) : base(dateTime.ToUniversalTime().Ticks.ToString(), Label, new Dictionary<string, object> { { "datetime", dateTime.ToString(format) },  { "ticks", dateTime.ToUniversalTime().Ticks }, { "format", format } }, publicationId, startPage, endPage, auditSessionId)
        {
            DateTime = dateTime;
        }

        public DateTime DateTime { get; }
    }
}
