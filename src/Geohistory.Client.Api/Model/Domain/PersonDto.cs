using Newtonsoft.Json;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Model.Domain;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.People;

namespace UK.CO.Itofinity.GeoHistory.Client.Api.Model.Domain
{
    public class PersonDto : AbstractDto, IPerson
    {
        public PersonDto()
        { }

        public PersonDto(string id, string name) : base(id)
        {
            Name = name;
        }

        [JsonProperty]
        public string Name { get; private set; }
    }
}
