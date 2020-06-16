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

        public double ControlFactor => 0.5;

        public double InfluenceFactor => 2.0;
    }
}
