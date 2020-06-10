using System.Globalization;

namespace Uk.Co.Itofinity.Geohistory.Model.Role.Military
{
    public abstract class AbstractMilitaryRole : IRole
    {
        protected AbstractMilitaryRole(RegionInfo country)
        {
            this.Country = country;
        }

        public RegionInfo Country { get; }
    }
}