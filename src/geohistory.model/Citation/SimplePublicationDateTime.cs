using System;
using UK.CO.Itofinity.GeoHistory.Model.Time;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class SimplePublicationDateTime : FuzzyDateTime
    {
        public SimplePublicationDateTime(DateTime dateTime, string format) : base(dateTime, format)
        {
        }
    }
}