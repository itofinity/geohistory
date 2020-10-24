using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using UK.CO.Itofinity.GeoHistory.Client.Api.Service;
using UK.CO.Itofinity.GeoHistory.Client.UI.Cli.Commands.Audit;
using UK.CO.Itofinity.GeoHistory.Client.UI.Cli.Commands.Storage;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand();
            var storageService = new GremlinStorageService();

            rootCommand.Add(new StorageCommand(storageService));
            rootCommand.Add(new SessionCommand(storageService));
            rootCommand.Add(new PublicationCommand(storageService));

            rootCommand.Handler = CommandHandler.Create(() =>
            {
                /* do something */
                Console.WriteLine("root");
                
            });

            rootCommand.InvokeAsync(args).Wait();
        }
    }
}
