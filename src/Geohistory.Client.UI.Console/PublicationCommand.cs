using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Geohistory.Client.UI.Cli;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli
{
    internal class PublicationCommand : Command
    {
        public const string NAME = "publication";
        public const string DESCRIPTION = "err stuff to do with publications";

        public PublicationCommand(IStorageService storageService) :base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Handler = CommandHandler.Create(() =>
            {
                /* do something */
                Console.WriteLine($"called {NAME}");

            });
            this.Add(new SearchGoogleBooksCommand(storageService));
        }

        public IStorageService StorageService { get; }
    }
}
