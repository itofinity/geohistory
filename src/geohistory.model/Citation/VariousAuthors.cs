using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public class VariousAuthors : List<IAuthor>
    {
        public VariousAuthors()
        {
            this.Add(new SimpleAuthor("Various"));
        }
    }
}
