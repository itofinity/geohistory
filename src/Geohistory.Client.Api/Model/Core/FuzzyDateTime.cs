using System;
using System.Collections.Generic;
using System.Text;
using UK.CO.Itofinity.GeoHistory.Spi.Core;

namespace UK.CO.Itofinity.GeoHistory.Client.Api.Model.Core
{
    public class FuzzyDateTime : IFuzzyDateTime
    {
        public FuzzyDateTime(DateTime dateTime, string format)
        {
            DateTime = dateTime;
            Format = format;
        }

        public DateTime DateTime { get; }
        public string Format { get; }
    }
}
