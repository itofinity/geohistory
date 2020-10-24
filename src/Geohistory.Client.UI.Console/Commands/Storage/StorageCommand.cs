using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli.Commands.Storage
{
    internal class StorageCommand : Command
    {
        public const string NAME = "storage";
        public const string DESCRIPTION = "err stuff to do with storage";

        public StorageCommand(IStorageService storageService) :base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Handler = CommandHandler.Create(() =>
            {
                /* do something */
                Console.WriteLine($"called {NAME}");

            });
            this.Add(new StorageClearCommand(storageService));
            this.Add(new StorageListCommand(storageService));
        }

        public IStorageService StorageService { get; }
    }
}
