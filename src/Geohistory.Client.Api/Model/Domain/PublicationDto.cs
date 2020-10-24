using Newtonsoft.Json;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Model.Domain;
using UK.CO.Itofinity.GeoHistory.Spi.Core;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication;

namespace UK.CO.Itofinity.GeoHistory.Client.Api.Model.Domain
{
    public class PublicationDto : AbstractDto, IPublication
    {
        public PublicationDto()
        { }

        public PublicationDto(string id, string title, string description, IFuzzyDateTime publishedDate) : base(id)
        {
            Title = title;
            Description = description;
            PublishedDate = publishedDate;
        }

        [JsonProperty]
        public string Title { get; private set; }

        public string Description { get; private set; }

        public IFuzzyDateTime PublishedDate { get; private set; }

        public string Name => Title;
    }
}
