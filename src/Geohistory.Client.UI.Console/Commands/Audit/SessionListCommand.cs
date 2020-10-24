using System;
using System.CommandLine;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using System.CommandLine.Invocation;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli.Commands.Audit
{
    public class SessionListCommand : Command
    {
        public const string NAME = "list";
        public const string DESCRIPTION = "list sessions";

        public SessionListCommand(IStorageService storageService) : base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Handler = CommandHandler.Create(async () =>
            {
                var result = await storageService.ListByTypeAsync(AuditSession.TYPE);
            });
        }

        public IStorageService StorageService { get; }
    }
}
