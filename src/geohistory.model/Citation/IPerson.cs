using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public interface IPerson
    {
        string FamilyName { get; }
        List<string> PersonalNames { get; }
        string ToShortString();
        string ToReverseString();
    }
}