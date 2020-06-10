using System;
using System.Collections.Generic;
using System.Linq;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    /// see https://pitt.libguides.com/c.php?g=12108&p=64730
    public class ApaCitation : ICitation
    {
        // TODO magic string
        // TODO i18n
        public const string Unknown = "Unknown";

        public ApaCitation(IPageRange pageRange, IPublication publication)
        {
            PageRange = pageRange;
            Publication = publication;
        }

        public IPageRange PageRange { get; }

        public IPublication Publication { get; }

        public string ToInTextString()
        {
            return  Publication.ToShortString();
        }

        public string ToDirectQuoteString()
        {
            return $"({GetShortAuthorsString(Publication.Authors)}, {Publication.PublicationDateTime.ToFormattedString()}, {SimplePageRange.GetPageRangeString(PageRange)})";
        }

        public override string ToString()
        {
            return Publication.ToString();
        }

        private static string GetShortAuthorsString(List<IAuthor> authors)
        {
            return string.Join(" ", authors.Select(a => a.ToShortString()));
        }
    }
}