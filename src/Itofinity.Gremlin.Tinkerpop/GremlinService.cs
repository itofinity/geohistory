﻿using AutoMapper;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using static Itofinity.Gremlin.Tinkerpop.ScriptBuilder;

namespace Itofinity.Gremlin.Tinkerpop
{
    public class GremlinService
    {
        private IMapper mapper;
        private ILogger logger;

        public GremlinService(string hostname, int port, string authKey, string database, string collection, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<GremlinService>();
            this.Hostname = hostname;
            this.Port = port;
            this.AuthKey = authKey;
            this.Database = database;
            this.Collection = collection;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Dictionary<string, object>, GremlinObject>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(s => s.ContainsKey("id") ? s["id"] : null))
                    .ForMember(dest => dest.Label,
                            opt => opt.MapFrom(s => s.ContainsKey("label") ? s["label"] : null))
                    .ForMember(dest => dest.Type,
                            opt => opt.MapFrom(s => s.ContainsKey("type") ? s["type"] : null))
                    .ForMember(dest => dest.Properties,
                            opt => opt.MapFrom(s => s.ContainsKey("properties") ? s["properties"] : null));
                cfg.CreateMap<KeyValuePair<string, object>, GremlinProperty>()
                     .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(s => s.Key))
                    .ForMember(dest => dest.Value,
                            opt => opt.MapFrom(s => s.Value));
            });
            mapper = config.CreateMapper();
        }

        public string Hostname { get; }
        public int Port { get; }
        public string AuthKey { get; }
        public string Database { get; }
        public string Collection { get; }
        public static List<string> DropScript
        {
            get
            {
                return new List<string>()
                {
                    g.E().drop().Build(),
                    g.V().drop().Build(),
                    // Doesn't work, Grelimn 3 perhaps ? g.commit().Build()
                };
            }
        }

        public async Task<Dictionary<string, object>> RunGremlinQueries(List<string> queries)
        {

            var results = new Dictionary<string, object>();

            using (var gremlinClient = GetGremlinClient())
            {
                foreach (var query in queries)
                {
                    results.Add(query, await RunQuery<Dictionary<string, object>>(gremlinClient, query));
                }
            }

            return results;
        }

        private GremlinClient GetGremlinClient()
        {
            return new GremlinClient(GetGremlinServer(), new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType);
        }

        public async Task<List<GremlinObject>> RunQuery(string query)
        {
            return mapper.Map<List<GremlinObject>>(await RunQuery<dynamic>(query));
        }

        public async Task<List<T>> RunQuery<T>(string query)
        {
            using (var gremlinClient = GetGremlinClient())
            {
                var result = await RunQuery<T>(gremlinClient, query);
                return (List<T>)result;
            }
        }

        public async Task<List<T>> RunQueries<T>(List<string> queries)
        {
            Log($"#{DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo)}");

            var results = await Task.WhenAll(queries.Select(async q => await RunQuery<T>(q)));
            return results.SelectMany(r => r).ToList();
        }

        private GremlinServer GetGremlinServer()
        {
            return new GremlinServer(Hostname, Port, enableSsl: false,
                    username: "/dbs/" + Database + "/colls/" + Collection,
                    password: AuthKey);
        }

        private async Task<List<T>> RunQuery<T>(GremlinClient gremlinClient, string query)
        {
            Log(query);

            // Create async task to execute the Gremlin query.
            var resultSet = await SubmitRequest<T>(gremlinClient, query);

            LogResultSet(resultSet);

            // Filter
            return resultSet.ToList();
        }

        private static void Log(string query)
        {
            using (StreamWriter w = File.AppendText("gremlin.script"))
            {
                w.WriteLine(query);
            }
        }

        private void LogResultSet<T>(ResultSet<T> resultSet)
        {
            if (resultSet.Count > 0)
            {
                logger.LogDebug("\tResult:");
                foreach (var result in resultSet)
                {
                    // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                    string output = JsonConvert.SerializeObject(result);
                    logger.LogDebug($"\t{output}");
                }
            }

            // Print the status attributes for the result set.
            // This includes the following:
            //  x-ms-status-code            : This is the sub-status code which is specific to Cosmos DB.
            //  x-ms-total-request-charge   : The total request units charged for processing a request.
            PrintStatusAttributes(resultSet.StatusAttributes);
        }

        private Task<ResultSet<T>> SubmitRequest<T>(GremlinClient gremlinClient, string query)
        {
            try
            {
                return gremlinClient.SubmitAsync<T>(query);
            }
            catch (ResponseException e)
            {
                logger.LogError("\tRequest Error!");

                // Print the Gremlin status code.
                logger.LogError($"\tStatusCode: {e.StatusCode}");

                // On error, ResponseException.StatusAttributes will include the common StatusAttributes for successful requests, as well as
                // additional attributes for retry handling and diagnostics.
                // These include:
                //  x-ms-retry-after-ms         : The number of milliseconds to wait to retry the operation after an initial operation was throttled. This will be populated when
                //                              : attribute 'x-ms-status-code' returns 429.
                //  x-ms-activity-id            : Represents a unique identifier for the operation. Commonly used for troubleshooting purposes.
                PrintStatusAttributes(e.StatusAttributes);
                logger.LogError($"\t[\"x-ms-retry-after-ms\"] : { GetValueAsString(e.StatusAttributes, "x-ms-retry-after-ms")}");
                logger.LogError($"\t[\"x-ms-activity-id\"] : { GetValueAsString(e.StatusAttributes, "x-ms-activity-id")}");

                throw;
            }
        }

        private void PrintStatusAttributes(IReadOnlyDictionary<string, object> attributes)
        {
            logger.LogDebug($"\tStatusAttributes:");
            logger.LogDebug($"\t[\"x-ms-status-code\"] : { GetValueAsString(attributes, "x-ms-status-code")}");
            logger.LogDebug($"\t[\"x-ms-total-request-charge\"] : { GetValueAsString(attributes, "x-ms-total-request-charge")}");
        }

        private static string GetValueAsString(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            return JsonConvert.SerializeObject(GetValueOrDefault(dictionary, key));
        }

        private static object GetValueOrDefault(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return null;
        }
    }
}
