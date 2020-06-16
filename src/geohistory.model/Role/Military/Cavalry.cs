using System;
using System.Collections.Generic;
using System.Text;
using Uk.Co.Itofinity.Geohistory.Model.Organisation.Military;

namespace Uk.Co.Itofinity.Geohistory.Model.Role.Military
{
    public class Cavalry : IPurpose
    {
        public string Name => Glossary.Cavalry;

        public string ShortName => Glossary.Cavalry;

        public double ControlFactor => 0.5;

        public double InfluenceFactor => 2.0;
    }
}
