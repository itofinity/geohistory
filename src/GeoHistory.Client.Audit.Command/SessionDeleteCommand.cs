using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using System.CommandLine.Invocation;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using System.ComponentModel.Composition;

namespace UK.CO.Itofinity.GeoHistory.Client.Commands.Audit
{
    [Export(typeof(ISessionCommand))]
    public class SessionDeleteCommand : Command, ISessionCommand
    {
        public const string NAME = "delete";
        public const string DESCRIPTION = "delete session";

        [ImportingConstructor]
        public SessionDeleteCommand(IStorageService storageService) : base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Add(new Option<string>("--id"));
            this.Add(new Option<bool>("--force"));

            this.Handler = CommandHandler.Create<string, bool>(async (id, force) => {
                var delete = force;
                if (!delete)
                {
                    Console.WriteLine($"Delete session [{id}] [Y/n] ?");
                    var storeResponse = Console.ReadLine();
                    delete = "y".Equals(storeResponse.ToLower());
                }
                if (delete)
                {
                    await storageService.DeleteAsync(AuditSession.TYPE, id);
                }
            } );
        }

        public IStorageService StorageService { get; }
    }
}
