using System;
namespace Uk.Co.Itofinity.GeoHistory.Model.Time
{
    public interface IFuzzyDateTime
    {
        DateTime DateTime { get; }
        string Format { get; }

        string ToYearString();
        string ToFormattedString();
    }
}