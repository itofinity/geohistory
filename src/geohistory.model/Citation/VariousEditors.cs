using System.Collections.Generic;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class VariousEditors : List<IEditor>
    {
        public VariousEditors()
        {
            this.Add(new SimpleEditor("Various"));
        }
    }
}
