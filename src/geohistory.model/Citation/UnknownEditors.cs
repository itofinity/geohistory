using System;
using System.Collections.Generic;
using System.Text;
using Uk.Co.Itofinity.Geohistory.Model.Citation;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class UnknownEditors : List<IEditor>
    {
        public UnknownEditors()
        {
            this.Add(new SimpleEditor("Unknown"));
        }
    }
}
