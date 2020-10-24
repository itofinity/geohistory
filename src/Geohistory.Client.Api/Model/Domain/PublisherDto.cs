using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Model.Domain;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Organisation.Commercial;

namespace UK.CO.Itofinity.GeoHistory.Client.Api.Model.Domain
{
    public class PublisherDto : AbstractDto, IPublisher
    {
        public PublisherDto()
        { }

        public PublisherDto(string id, string name) : base(id)
        {
            Name = name;
        }

        [JsonProperty]
        public string Name { get; private set; }
    }
}
