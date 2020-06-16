using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.Geohistory.Model
{
    public interface INamed
    {
        string Name { get; }

        string ShortName { get; }
    }
}
