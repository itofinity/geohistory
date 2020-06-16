using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.Geohistory.Model.Time
{
    public class FuzzyDateRange :IFuzzyDateTimeRange
    {

        public FuzzyDateRange(IFuzzyDateTime startDateTime) : this(startDateTime, null)
        {
        }

        public FuzzyDateRange(IFuzzyDateTime startDateTime, IFuzzyDateTime endDateTime)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

        public FuzzyDateRange(FuzzyDateTime startDateTime, int daysSpan)
        {
            StartDateTime = startDateTime;
            EndDateTime = new FuzzyDateTime(startDateTime.DateTime.AddDays(daysSpan), startDateTime.Format);
        }

        public IFuzzyDateTime StartDateTime { get; }
        public IFuzzyDateTime EndDateTime { get; private set; }

        public string Name => ToString();

        public string ShortName => ToString();

        public void FixEndDate(IFuzzyDateTime revisedDateTime)
        {
            EndDateTime = revisedDateTime;
        }

        public override string ToString()
        {
            return $"{StartDateTime.DateTime.ToString(StartDateTime.Format)}";
        }
    }
}
