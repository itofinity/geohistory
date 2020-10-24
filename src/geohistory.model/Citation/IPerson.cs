using System.Collections.Generic;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public interface IPerson
    {
        string FamilyName { get; }
        List<string> PersonalNames { get; }
        string ToShortString();
        string ToReverseString();
    }
}