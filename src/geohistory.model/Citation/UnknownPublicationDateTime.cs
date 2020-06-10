using System;
using System.Collections.Generic;
using System.Text;
using Uk.Co.Itofinity.Geohistory.Model;
using Uk.Co.Itofinity.Geohistory.Model.Time;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class UnknownPublicationDateTime : IFuzzyDateTime
    {
        public DateTime DateTime => DateTime.MaxValue;

        public string Format => "";

        public string ToFormattedString()
        {
            return "Unknown";
        }

        public string ToYearString()
        {
            return "Unknown";
        }
    }
}
