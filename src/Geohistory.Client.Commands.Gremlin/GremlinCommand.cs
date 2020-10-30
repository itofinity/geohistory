using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel.Composition;
using System.Linq;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;

namespace UK.CO.Itofinity.GeoHistory.Client.Commands.Gremlin
{
    [Export(typeof(ICommand))]
    internal class GremlinCommand : Command
    {
        public const string NAME = "gremlin";
        public const string DESCRIPTION = "err stuff to do with gremlin";

        [ImportingConstructor]
        public GremlinCommand(IStorageService storageService, [ImportMany] IEnumerable<IGremlinCommand> subCommands) :base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Handler = CommandHandler.Create(() =>
            {
                /* do something */
                Console.WriteLine($"called {NAME}");

            });

            subCommands.ToList().ForEach(sc => this.Add((Symbol)sc));
        }

        public IStorageService StorageService { get; }
    }
}
