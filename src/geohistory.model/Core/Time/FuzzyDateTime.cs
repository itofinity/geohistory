using System;
using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model.Domain;

namespace Uk.Co.Itofinity.GeoHistory.Model.Core.Time
{
    public class FuzzyDateTime : AbstractPropertyEntity
    {
        public const string Label = "datetime";

        public FuzzyDateTime(DateTime dateTime, string citationId, string auditSessionId) : this(dateTime, "F", citationId, auditSessionId)
        {
        }

        public FuzzyDateTime(DateTime dateTime, string format, string citationId, string auditSessionId) : base(dateTime.ToUniversalTime().Ticks.ToString(), Label, new Dictionary<string, object> { { "datetime", dateTime.ToString(format) }, { "ticks", dateTime.ToUniversalTime().Ticks }, { "format", format } }, citationId, auditSessionId)
        {
        }
    }
}
