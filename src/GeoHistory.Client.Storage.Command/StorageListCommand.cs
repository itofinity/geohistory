using System;
using System.CommandLine;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using System.CommandLine.Invocation;
using System.ComponentModel.Composition;
using UK.CO.Itofinity.GeoHistory.Client.Commands.Storage;

namespace UK.CO.Itofinity.GeoHistory.Client.Commands.Storage
{
    [Export(typeof(IStorageCommand))]
    public class StorageListCommand : Command, IStorageCommand
    {
        public const string NAME = "list";
        public const string DESCRIPTION = "list storage";

        [ImportingConstructor]
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
