using System.Collections.Generic;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class SimpleEditor : AbstractPerson, IEditor
    {
        public SimpleEditor(string familyName) : this(familyName, new List<string>())
        {
        }

        public SimpleEditor(string familyName, List<string> personalNames) : base(familyName, personalNames)
        {
        }
    }
}