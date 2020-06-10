using System.Globalization;

namespace Uk.Co.Itofinity.Geohistory.Model.Organisation.Military
{
    public class Regiment : AbstractOrganisation
    {
        public Regiment(string name, string purpose, RegionInfo country) : base(name, purpose, country)
        {
        }

        public override string Size => Glossary.Regiment;
    }
}