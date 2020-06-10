using System.Collections.Generic;
using System.Linq;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public abstract class AbstractPerson : IPerson
    {
        
        // TODO magic string
        // TODO i18n
        public const string Unknown = "Unknown";

        public AbstractPerson(string familyName, List<string> personalNames)
        {
            PersonalNames = personalNames;
            FamilyName = familyName;
        }

        public List<string> PersonalNames { get; }
        public string FamilyName { get; }

        public string ToShortString()
        {
            return FamilyName != null ? FamilyName : Unknown;
        }

        public override string ToString()
        {
            return $"{FamilyName}, {string.Join(" ", PersonalNames.Select(pn => GetInitial(pn)))}";
        }

        public string ToReverseString()
        {
            return $"{string.Join($" ", PersonalNames.Select(pn => GetInitial(pn)))} {FamilyName}";
        }

        public static string GetInitial(string name)
        {
            return !string.IsNullOrWhiteSpace(name) ? $"{name.ToCharArray()[0]}." : string.Empty;
        }
    }
}