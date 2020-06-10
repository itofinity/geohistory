using System.Globalization;

namespace Uk.Co.Itofinity.Geohistory.Model.Organisation.Military
{
    public class Division : AbstractOrganisation
    {
        public Division(RegionInfo country) : base(country)
        {
        }

        public override string Size => Glossary.Division;
    }
}