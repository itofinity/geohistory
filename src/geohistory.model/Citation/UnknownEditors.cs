using System.Collections.Generic;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class UnknownEditors : List<IEditor>
    {
        public UnknownEditors()
        {
            this.Add(new SimpleEditor("Unknown"));
        }
    }
}
