using System.Collections.Generic;
using Uk.Co.Itofinity.GeoHistory.Model.Time;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
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