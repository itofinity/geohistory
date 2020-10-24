using UK.CO.Itofinity.GeoHistory.Model.Organisation.Military;

namespace UK.CO.Itofinity.GeoHistory.Model.Role.Military
{
    public class Reconnaissance : IPurpose
    {
        public string Name => Glossary.Reconnaissance;

        public string ShortName => Glossary.Reconnaissance;

        public double ControlFactor => 1.0;

        public double InfluenceFactor => 10.0;
    }
}
