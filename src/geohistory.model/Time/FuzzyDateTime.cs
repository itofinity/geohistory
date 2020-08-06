using System;

namespace Uk.Co.Itofinity.GeoHistory.Model.Time
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

        public string ToYearString()
        {
            return DateTime.Year.ToString();
        }

        public string ToFormattedString()
        {
            return DateTime.ToString(Format);
        }
    }
}