using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Organisation.Commercial;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.People;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication;

namespace UK.CO.Itofinity.GeoHistory.Client.Spi.Service
{
    public interface IPublicationService
    {
        Task<List<IPublication>> Query(string title);
        Task<IPublication> Add(IPublication book);
        Task<Dictionary<string, List<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>>> Search(IPublication publicationTerms);
    }
}
