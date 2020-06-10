using System;
using System.Collections.Generic;
using System.Text;
using Uk.Co.Itofinity.Geohistory.Model.Citation;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class VariousEditors : List<IEditor>
    {
        public VariousEditors()
        {
            this.Add(new SimpleEditor("Various"));
        }
    }
}
