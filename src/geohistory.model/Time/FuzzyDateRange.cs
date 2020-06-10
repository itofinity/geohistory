using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.Geohistory.Model.Time
{
    public class FuzzyDateRange : AbstractTemporal
    {
        public FuzzyDateRange(IFuzzyDateTime startDateTime) : this(startDateTime, null)
        {
        }

        public FuzzyDateRange(IFuzzyDateTime startDateTime, IFuzzyDateTime endDateTime) : base(startDateTime, endDateTime)
        {
        }
    }
}
