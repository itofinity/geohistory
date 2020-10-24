using System;
using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain;
using UK.CO.Itofinity.GeoHistory.Spi.Core;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core.Time
{
    public class FuzzyDateTime : AbstractPropertyEntity, IFuzzyDateTime
    {
        public const string Label = "datetime";

        public FuzzyDateTime(DateTime dateTime, string citationId, string auditSessionId) : this(dateTime, "F", citationId, auditSessionId)
        {
        }

        public FuzzyDateTime(DateTime dateTime, string format, string citationId, string auditSessionId) : base(dateTime.ToUniversalTime().Ticks.ToString(), Label, new Dictionary<string, object> { { "datetime", dateTime.ToString(format) },  { "ticks", dateTime.ToUniversalTime().Ticks }, { "format", format } }, citationId, auditSessionId)
        {
            DateTime = dateTime;
        }

        public DateTime DateTime { get; }
    }
}
