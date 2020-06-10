using System.Collections.Generic;
using System.Linq;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class SimpleAuthor : AbstractPerson, IAuthor
    {
        public SimpleAuthor(string familyName) : this(familyName, new List<string>())
        {
        }

        public SimpleAuthor(string familyName, List<string> personalNames) : base(familyName, personalNames)
        {
        }
    }
}