using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.Geohistory.Model.Time
{
    public interface IFuzzyDateTimeRange : INamed
    {
        IFuzzyDateTime StartDateTime { get; }
        IFuzzyDateTime EndDateTime { get; }

        void FixEndDate(IFuzzyDateTime startDateTime);
    }
}
