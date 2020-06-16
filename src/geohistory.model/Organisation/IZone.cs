using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.Geohistory.Model.Organisation
{
    public interface IZone
    {
        double Range { get; }
        double Density { get; }
    }
}
