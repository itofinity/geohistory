using System.Globalization;

namespace UK.CO.Itofinity.GeoHistory.Model.Organisation.Military
{
    public class Brigade : AbstractOrganisation
    {
        public Brigade(RegionInfo country) : base(country)
        {
        }

        public override string Size => Glossary.Brigade;
    }
}