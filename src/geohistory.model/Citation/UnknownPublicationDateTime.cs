using System;
using Uk.Co.Itofinity.GeoHistory.Model.Time;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
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
