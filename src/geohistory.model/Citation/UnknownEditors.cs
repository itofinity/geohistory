using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public class UnknownEditors : List<IEditor>
    {
        public UnknownEditors()
        {
            this.Add(new SimpleEditor("Unknown"));
        }
    }
}
