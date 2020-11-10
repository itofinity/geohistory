using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Itofinity.Geohistory.Spi.Domain;
using Itofinity.Gremlin.Tinkerpop;
using Microsoft.Extensions.Logging;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin;

using static tinkerpop.scripts.ScriptBuilder;
using static tinkerpop.scripts.ScriptClauses;

namespace UK.CO.Itofinity.GeoHistory.Client.Api.Service
{
    [Export(typeof(IStorageService))]
    public class GremlinStorageService : IStorageService
    {
        private ILogger<GremlinStorageService> logger;

        // Azure Cosmos DB Configuration variables
        // Replace the values in these variables to your own.
        private const string DEFAULT_HOSTNAME = "localhost";
        private const int DEFAULT_PORT = 8901;
        private const string DEFAULT_AUTHKEY = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private const string DEFAULT_DATABASE = "geohistory"; // avoid issues make everything lowercase
        private const string DEFAULT_COLLECTION = "container1"; // avoid issues make everything lowercase

        [ImportingConstructor]
        public GremlinStorageService(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<GremlinStorageService>();
            GremlinService = new GremlinService(DEFAULT_HOSTNAME, DEFAULT_PORT, DEFAULT_AUTHKEY, DEFAULT_DATABASE, DEFAULT_COLLECTION, loggerFactory);
        }

        public GremlinService GremlinService { get; }

        public async Task ClearAsync()
        {
            var result = await GremlinService.RunQueries<dynamic>(GremlinService.DropScript.Where(s => s.Contains("E()")).ToList());
            var result2 = await GremlinService.RunQueries<dynamic>(GremlinService.DropScript.Where(s => s.Contains("V()")).ToList());
        }

        public Task DeleteAsync(string type, string id)
        {
            throw new NotImplementedException();
        }

        public async Task<object> ListByTypeAsync(string type)
        {
            var queries = new List<string>();
            queries.Add(g.V().hasLabel(type).Build());
            var result = await GremlinService.RunQueries<dynamic>(queries);
            return result;
        }

        public async Task StoreAsync(IQuery item)
        {
            // convert to gremlin script
            // run script
            var findResult = await GremlinService.RunQuery<dynamic>(item.ToFindQuery());

            try
            {
                var insertResult = await GremlinService.RunQueries<dynamic>(item.ToInsertQueries());
            }
            catch(Exception ex)
            {
                logger.LogError($"Insert [{item.ToInsertQueries()}] failed due to {ex.Message}");
            }
        }

        public async Task StoreAsync(IEnumerable<IQuery> items)
        {
            var queries = items.SelectMany(i => i.ToInsertQueries());

            var result = await GremlinService.RunQueries<dynamic>(queries.ToList());
        }
    }
}
