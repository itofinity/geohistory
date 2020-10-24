using System;
using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Time;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class SimpleWebsite : IPublication
    {
        public SimpleWebsite(Uri uri)
        {

        }

        public string Name => throw new NotImplementedException();

        public List<IEditor> Editors => throw new NotImplementedException();

        public List<IAuthor> Authors => throw new NotImplementedException();

        public IFuzzyDateTime PublicationDateTime => throw new NotImplementedException();

        public IPublisher Publisher => throw new NotImplementedException();

        public string ToShortString()
        {
            throw new NotImplementedException();
        }
    }
}