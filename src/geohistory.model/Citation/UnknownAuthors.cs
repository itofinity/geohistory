using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public class UnknownAuthors : List<IAuthor>
    {
        public UnknownAuthors()
        {
            this.Add(new SimpleAuthor("Unknown"));
        }
    }
}
