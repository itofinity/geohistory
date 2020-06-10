using System.Globalization;

namespace Uk.Co.Itofinity.Geohistory.Model.Organisation.Military
{
    public class Battalion : AbstractOrganisation
    {
        public Battalion(RegionInfo country) : base(country)
        {
        }

        public override string Size => Glossary.Battalion;
    }
}