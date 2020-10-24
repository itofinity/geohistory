using UK.CO.Itofinity.GeoHistory.Model.Organisation.Military;

namespace UK.CO.Itofinity.GeoHistory.Model.Role.Military
{
    public class Cavalry : IPurpose
    {
        public string Name => Glossary.Cavalry;

        public string ShortName => Glossary.Cavalry;

        public double ControlFactor => 0.5;

        public double InfluenceFactor => 2.0;
    }
}
