using System.Collections.Generic;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class VariousAuthors : List<IAuthor>
    {
        public VariousAuthors()
        {
            this.Add(new SimpleAuthor("Various"));
        }
    }
}
