using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Time;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public interface IPublication
    {
        string Name { get; }
        List<IEditor> Editors { get; }
        List<IAuthor> Authors { get; }
        IFuzzyDateTime PublicationDateTime { get; }
        IPublisher Publisher { get; }

        string ToShortString();
    }
}