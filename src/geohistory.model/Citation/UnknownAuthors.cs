using System.Collections.Generic;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class UnknownAuthors : List<IAuthor>
    {
        public UnknownAuthors()
        {
            this.Add(new SimpleAuthor("Unknown"));
        }
    }
}
