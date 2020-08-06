using System;
using Uk.Co.Itofinity.GeoHistory.Model.Time;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public class SimplePublicationDateTime : FuzzyDateTime
    {
        public SimplePublicationDateTime(DateTime dateTime, string format) : base(dateTime, format)
        {
        }
    }
}