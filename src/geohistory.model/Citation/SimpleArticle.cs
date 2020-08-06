using System.Collections.Generic;
using System.Linq;
using Uk.Co.Itofinity.GeoHistory.Model.Time;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public class SimpleArticle : IArticle
    {
        public SimpleArticle(string title, IPublication publication, IPageRange pageRange)
        {
            Title = title;
            HostPublication = publication;
            PageRange = pageRange;
        }

        public string Title { get; }
        public IPublication HostPublication { get; }
        public IPageRange PageRange { get; }
        public string Name => HostPublication.Name;
        public List<IEditor> Editors => HostPublication.Editors;

        public List<IAuthor> Authors => HostPublication.Authors;
        public IFuzzyDateTime PublicationDateTime => HostPublication.PublicationDateTime;
        public IPublisher Publisher => HostPublication.Publisher;

        public string ToShortString()
        {
            return $"{SimplePublication.GetPeopleString(HostPublication.Authors.ToList<IPerson>())} ({HostPublication.PublicationDateTime.ToFormattedString()}). {HostPublication.Name}. {HostPublication.Publisher.ToString()}";
        }

        public override string ToString()
        {
            return $"{SimplePublication.GetPeopleString(HostPublication.Authors.ToList<IPerson>())} ({HostPublication.PublicationDateTime.ToFormattedString()}). {Title}. In {SimplePublication.GetReversePeopleString(HostPublication.Editors.ToList<IPerson>())} (Eds.), {HostPublication.Name} ({PageRange}). {HostPublication.Publisher}";
        }
    }
}