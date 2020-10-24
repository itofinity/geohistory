using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli.Commands.Audit
{
    internal class SessionCommand : Command
    {
        public const string NAME = "session";
        public const string DESCRIPTION = "err stuff to do with session";

        public SessionCommand(IStorageService storageService) :base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Handler = CommandHandler.Create(() =>
            {
                /* do something */
                Console.WriteLine($"called {NAME}");

            });
            this.Add(new SessionListCommand(storageService));
            this.Add(new SessionNewCommand(storageService));
            this.Add(new SessionDeleteCommand(storageService));
        }

        public IStorageService StorageService { get; }
    }
}
