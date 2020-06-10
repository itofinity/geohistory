using System;
using System.Collections.Generic;
using System.Text;
using Uk.Co.Itofinity.Geohistory.Model.Citation;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class UnknownAuthors : List<IAuthor>
    {
        public UnknownAuthors()
        {
            this.Add(new SimpleAuthor("Unknown"));
        }
    }
}
