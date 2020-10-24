namespace UK.CO.Itofinity.GeoHistory.Client.Spi.Model.Domain
{
    public abstract class AbstractDto : IDto
    {
        public AbstractDto()
        { }

        public AbstractDto(string id)
        {
            Id = id;
        }

        public string Id { get; private set;  }
    }
}
