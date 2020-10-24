using System;
using System.CommandLine;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using System.CommandLine.Invocation;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli.Commands.Storage
{
    public class StorageListCommand : Command
    {
        public const string NAME = "list";
        public const string DESCRIPTION = "list storage";

        public StorageListCommand(IStorageService storageService) : base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Add(new Option<string>("--type"));

            this.Handler = CommandHandler.Create<string>(async (type) =>
            {
                var result = await storageService.ListByTypeAsync(type);
            });
        }

        public IStorageService StorageService { get; }
    }
}
