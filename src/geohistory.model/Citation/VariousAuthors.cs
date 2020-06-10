using System;
using System.Collections.Generic;
using System.Text;
using Uk.Co.Itofinity.Geohistory.Model.Citation;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class VariousAuthors : List<IAuthor>
    {
        public VariousAuthors()
        {
            this.Add(new SimpleAuthor("Various"));
        }
    }
}
