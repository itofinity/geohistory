using System;
using System.Collections.Generic;
using System.Text;
using Uk.Co.Itofinity.Geohistory.Model.Organisation.Military;

namespace Uk.Co.Itofinity.Geohistory.Model.Role.Military
{
    public class Reconnaissance : IPurpose
    {
        public string Name => Glossary.Reconnaissance;

        public string ShortName => Glossary.Reconnaissance;

        public double ControlFactor => 1.0;

        public double InfluenceFactor => 10.0;
    }
}
