using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UK.CO.Itofinity.GeoHistory.Client.Api.Utils;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Organisation.Commercial;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.People;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication;

namespace UK.CO.Itofinity.GeoHistory.Client.Api.Service
{
    public class DefaultPublicationService : IPublicationService
    {
        public DefaultPublicationService(IStorageService storageService, ISearchService<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>> searchService)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            SearchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
        }

        public IStorageService StorageService { get; }
        public ISearchService<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>> SearchService { get; }

        public Task<IPublication> Add(IPublication book)
        {
            throw new NotImplementedException();
        }

        public Task<List<IPublication>> Query(string title)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<string,List<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>>> Search(IPublication publicationTerms)
        {
            
            var results = new Dictionary<string, List<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>> ();

            // storage
            results.AddRange(await SearchStorage(publicationTerms));

            // Google
            results.AddRange(await SearchGoogle(publicationTerms));

            return results;
        }

        private async Task<Dictionary<string, List<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>>> SearchGoogle(IPublication publicationTerms)
        {
            return await Task.FromResult(new Dictionary<string, List<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>> ());
        }

        private async Task<Dictionary<string, List<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>>> SearchStorage(IPublication publicationTerms)
        {
            var results = await SearchService.Search(publicationTerms.Title, 0, 10);
            return new Dictionary<string, List<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>> () { { SearchService.Name, results } }; 
        }
    }
}
