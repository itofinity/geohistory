using Uk.Co.Itofinity.GeoHistory.Model.Organisation.Military;
using Uk.Co.Itofinity.GeoHistory.Model.People;

namespace Uk.Co.Itofinity.GeoHistory.Model.Role.Military
{
    public class LieutenantColonel : AbstractMilitaryAppointment
    {
        public LieutenantColonel(IOrganisation organisation, Person person) : base(organisation, person)
        {
        }

        public override string Name => ToString();

        public override string ShortName => ToString();

        public override double ControlFactor => 1.0;

        public override double InfluenceFactor => 1.0;

        public override string ToString()
        {
            return $"{Glossary.LieutenantColonel} {Person}";
        }
    }
}