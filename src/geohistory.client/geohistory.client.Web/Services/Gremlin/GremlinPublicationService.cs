using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Itofinity.Gremlin.Tinkerpop;
using UK.CO.Itofinity.GeoHistory.Client.Api.Model.Domain;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;
using static tinkerpop.scripts.ScriptBuilder;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin;

namespace UK.CO.Itofinity.GeoHistory.Client.Web.Services.Gremlin
{
    public class GremlinPublicationService : IStorageService
    {
        // Azure Cosmos DB Configuration variables
        // Replace the values in these variables to your own.
        private const string DEFAULT_HOSTNAME = "localhost";
        private const int DEFAULT_PORT = 8901;
        private const string DEFAULT_AUTHKEY = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private const string DEFAULT_DATABASE = "sandbox1 NOT USED";
        private const string DEFAULT_COLLECTION = "container1";

        private List<string> defaultFields = new List<string>(){ "id", "label", "name" };

        private IMapper mapper;

        public GremlinPublicationService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GremlinObject, PublicationDto>()
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(s => GetTitle(s)));
                cfg.CreateMap<PublicationDto, Publication>();
            });
            mapper = config.CreateMapper();
        }

        private static dynamic GetTitle(GremlinObject go)
        {
            // brittle but so far the properties are dynamic so...
            var nameDict = (Dictionary<string, object>)go.Properties["name"][0];
            return nameDict["value"];
        }

        public async Task<IPublication> Add(IPublication book)
        {
            var gremlinService = new GremlinService(DEFAULT_HOSTNAME, DEFAULT_PORT, DEFAULT_AUTHKEY, DEFAULT_DATABASE, DEFAULT_COLLECTION);

           
            var publication = mapper.Map<Publication>(book);
            return mapper.Map<PublicationDto>(await gremlinService.RunQueries<dynamic>(publication.ToInsertQueries()));
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task<object> ListByTypeAsync(string type)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IPublication>> Query(string title) => await Query(title, new List<string>());

        public async Task<List<IPublication>> Query(string title, List<string> fieldNames)
        {

            //https://www.nuget.org/packages/FuzzyString
            var gremlinService = new GremlinService(DEFAULT_HOSTNAME, DEFAULT_PORT, DEFAULT_AUTHKEY, DEFAULT_DATABASE, DEFAULT_COLLECTION);

            var queryBuilder = g.V().hasLabel(Publication.Label);
            if (fieldNames.Any())
            {
                queryBuilder.values(fieldNames.ToArray());
            }

            var results = await gremlinService.RunQuery(queryBuilder.Build());

            var result = new Dictionary<string, object>();

            fieldNames.ForEach(key => 
                {
                    var index = fieldNames.IndexOf(key);
                    var value = results[index];
                    result.Add(key, value);
                });

            return mapper.Map<List<PublicationDto>>(results).ToList<IPublication>();
        }

        public Task<List<IPublication>> Search(string title)
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(IQuery item)
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(IEnumerable<IQuery> items)
        {
            throw new NotImplementedException();
        }
    }
}
