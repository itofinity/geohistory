using System;
using Uk.Co.Itofinity.Geohistory.Model.Time;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class SimplePublicationDateTime: FuzzyDateTime
    {
        public SimplePublicationDateTime(DateTime dateTime, string format) : base (dateTime, format)
        {
        }
    }
}