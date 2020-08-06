using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public class VariousEditors : List<IEditor>
    {
        public VariousEditors()
        {
            this.Add(new SimpleEditor("Various"));
        }
    }
}
