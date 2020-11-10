using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using System.CommandLine.Invocation;
using System.ComponentModel.Composition;
using System.IO;
using Itofinity.Gremlin.Tinkerpop;
using Microsoft.Extensions.Logging;

namespace UK.CO.Itofinity.GeoHistory.Client.Commands.Gremlin
{
    // TODO move to a Gremlin specific project
    [Export(typeof(IGremlinCommand))]
    public class GremlinImportCommand : Command, IGremlinCommand
    {
        public const string NAME = "import";
        public const string DESCRIPTION = "import gremlin script";

        // Azure Cosmos DB Configuration variables
        // Replace the values in these variables to your own.
        private const string DEFAULT_HOSTNAME = "localhost";
        private const int DEFAULT_PORT = 8901;
        private const string DEFAULT_AUTHKEY = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private const string DEFAULT_DATABASE = "geohistory"; // avoid issues make everything lowercase
        private const string DEFAULT_COLLECTION = "container1"; // avoid issues make everything lowercase
        private ILogger<GremlinService> logger;

        [ImportingConstructor]
        public GremlinImportCommand(IStorageService storageService, ILoggerFactory loggerFactory) : base(NAME, DESCRIPTION)
        {
            logger = loggerFactory.CreateLogger<GremlinService>();
            GremlinService = new GremlinService(DEFAULT_HOSTNAME, DEFAULT_PORT, DEFAULT_AUTHKEY, DEFAULT_DATABASE, DEFAULT_COLLECTION, loggerFactory);

            this.Add(new Option<string>("--file"));

            this.Handler = CommandHandler.Create<string>(async (file) => {
                using (StreamReader sr = File.OpenText(file))
                {
                    string line = null;
                    do
                    {
                        line = sr.ReadLine();
                        if(!String.IsNullOrWhiteSpace(line)
                            && !line.StartsWith("#"))
                        {
                            await GremlinService.RunQuery(line);
                        }
                    } while (!String.IsNullOrWhiteSpace(line));
                   
                }
            } );
        }

        public GremlinService GremlinService { get; }
    }
}
