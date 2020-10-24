using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static tinkerpop.scripts.ScriptBuilder;

namespace tinkerpop.scripts.Engine
{
    public class GremlinService
    {
        public GremlinService(string hostname, int port, string authKey, string database, string collection)
        {
            this.Hostname = hostname;
            this.Port = port;
            this.AuthKey = authKey;
            this.Database = database;
            this.Collection = collection;
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
                    g.V().drop().Build(),
                    g.E().drop().Build()
                };
            }
        }

        public void RunGremlinQueries(List<string> queries)
        {
            var gremlinServer = new GremlinServer(Hostname, Port, enableSsl: false,
                                                                            username: "/dbs/" + Database + "/colls/" + Collection,
                                                                            password: AuthKey);

            using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                foreach (var query in queries)
                {
                    RunGremlinQuery(gremlinClient, query);
                }
            }
        }

        public void RunGremlinQuery(GremlinClient gremlinClient, string query)
        {
            System.Console.WriteLine(String.Format("Running this query: {0}", query));

            // Create async task to execute the Gremlin query.
            var resultSet = SubmitRequest(gremlinClient, query).Result;

            if (resultSet.Count > 0)
            {
                System.Console.WriteLine("\tResult:");
                foreach (var result in resultSet)
                {
                    // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                    string output = JsonConvert.SerializeObject(result);
                    System.Console.WriteLine($"\t{output}");
                }
                System.Console.WriteLine();
            }

            // Print the status attributes for the result set.
            // This includes the following:
            //  x-ms-status-code            : This is the sub-status code which is specific to Cosmos DB.
            //  x-ms-total-request-charge   : The total request units charged for processing a request.
            PrintStatusAttributes(resultSet.StatusAttributes);
            System.Console.WriteLine();
        }

        public Task<ResultSet<Dictionary<string, object>>> SubmitRequest(GremlinClient gremlinClient, string query)
        {
            try
            {
                return gremlinClient.SubmitAsync<Dictionary<string, object>>(query);
            }
            catch (ResponseException e)
            {
                System.Console.WriteLine("\tRequest Error!");

                // Print the Gremlin status code.
                System.Console.WriteLine($"\tStatusCode: {e.StatusCode}");

                // On error, ResponseException.StatusAttributes will include the common StatusAttributes for successful requests, as well as
                // additional attributes for retry handling and diagnostics.
                // These include:
                //  x-ms-retry-after-ms         : The number of milliseconds to wait to retry the operation after an initial operation was throttled. This will be populated when
                //                              : attribute 'x-ms-status-code' returns 429.
                //  x-ms-activity-id            : Represents a unique identifier for the operation. Commonly used for troubleshooting purposes.
                PrintStatusAttributes(e.StatusAttributes);
                System.Console.WriteLine($"\t[\"x-ms-retry-after-ms\"] : { GetValueAsString(e.StatusAttributes, "x-ms-retry-after-ms")}");
                System.Console.WriteLine($"\t[\"x-ms-activity-id\"] : { GetValueAsString(e.StatusAttributes, "x-ms-activity-id")}");

                throw;
            }
        }

        private static void PrintStatusAttributes(IReadOnlyDictionary<string, object> attributes)
        {
            System.Console.WriteLine($"\tStatusAttributes:");
            System.Console.WriteLine($"\t[\"x-ms-status-code\"] : { GetValueAsString(attributes, "x-ms-status-code")}");
            System.Console.WriteLine($"\t[\"x-ms-total-request-charge\"] : { GetValueAsString(attributes, "x-ms-total-request-charge")}");
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
