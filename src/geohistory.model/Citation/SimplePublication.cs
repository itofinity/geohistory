using System.Collections.Generic;
using System.Linq;
using UK.CO.Itofinity.GeoHistory.Model.Time;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class SimplePublication : IPublication
    {
        public SimplePublication(string name, List<IAuthor> authors, List<IEditor> editors, IFuzzyDateTime publicationDateTime, IPublisher publisher)
        {
            Name = name;
            Authors = authors;
            Editors = editors;
            PublicationDateTime = publicationDateTime;
            Publisher = publisher;
        }

        public string Name { get; }
        public List<IEditor> Editors { get; }
        public List<IAuthor> Authors { get; }
        public IFuzzyDateTime PublicationDateTime { get; }
        public IPublisher Publisher { get; }

        public string ToShortString()
        {
            return $"({GetShortPeopleString(Authors.ToList<IPerson>())}, {PublicationDateTime.ToFormattedString()})";
        }

        public override string ToString()
        {
            return $"{GetPeopleString(Authors.ToList<IPerson>())} ({PublicationDateTime.ToFormattedString()}). {Name}. {Publisher.ToString()}";
        }


        public static string GetPeopleString(List<IPerson> people)
        {
            return string.Join(" ", people.Select(a => a.ToString()));
        }

        public static string GetShortPeopleString(List<IPerson> people)
        {
            return string.Join(" ", people.Select(a => a.ToShortString()));
        }

        public static string GetReversePeopleString(List<IPerson> people)
        {
            return GetReversePeopleString(people, "&");
        }

        public static string GetReversePeopleString(List<IPerson> people, string seperator)
        {
            return string.Join($" {seperator} ", people.Select(a => a.ToReverseString()));
        }
    }
}