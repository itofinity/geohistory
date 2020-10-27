using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel.Composition;
using System.Linq;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;

namespace UK.CO.GeoHistory.Client.Commands.Search
{
    [Export(typeof(ICommand))]
    internal class SearchCommand : Command
    {
        public const string NAME = "search";
        public const string DESCRIPTION = "err stuff to do with search";

        [ImportingConstructor]
        public SearchCommand([ImportMany] IEnumerable<ISearchCommand> subCommands) :base(NAME, DESCRIPTION)
        {
            this.Handler = CommandHandler.Create(() =>
            {
                /* do something */
                Console.WriteLine($"called {NAME}");

            });
            subCommands.ToList().ForEach(sc => this.Add((Symbol)sc));
        }
    }
}
