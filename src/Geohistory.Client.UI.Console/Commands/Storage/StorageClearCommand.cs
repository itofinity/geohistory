using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using System.CommandLine.Invocation;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli.Commands.Storage
{
    public class StorageClearCommand : Command
    {
        public const string NAME = "clear";
        public const string DESCRIPTION = "clear storage";

        public StorageClearCommand(IStorageService storageService) : base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Add(new Option<string>("--force"));

            this.Handler = CommandHandler.Create<bool>(async (force) => {
                var clear = force;
                if (!clear)
                {
                    Console.WriteLine($"Clear storage [Y/n] ?");
                    var storeResponse = Console.ReadLine();
                    clear = "y".Equals(storeResponse.ToLower());
                }
                if (clear)
                {
                    await storageService.ClearAsync();
                }
            } );
        }

        public IStorageService StorageService { get; }
    }
}
