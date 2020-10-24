using System;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class SimpleWebPublisher : IPublisher
    {
        public SimpleWebPublisher(Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; }

        public override string ToString()
        {
            return $"Retrieved from {Uri.AbsoluteUri}";
        }
    }
}