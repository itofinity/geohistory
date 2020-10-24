using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using System.CommandLine.Invocation;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Time;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.People;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core.Time;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli.Commands.Audit
{
    public class SessionNewCommand : Command
    {
        public const string NAME = "new";
        public const string DESCRIPTION = "new session";

        public SessionNewCommand(IStorageService storageService) : base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Add(new Option<string>("--name"));
            this.Add(new Option<string>("--email"));
            this.Add(new Option<string>("--title"));

            this.Handler = CommandHandler.Create<string, string, string>(async (name, email, title) => {
                var items = new List<IQuery>();
                var auditSessionSet = CreateAuditingSessionSet(name, email, title, DateTime.Now);
                items.AddRange(auditSessionSet);

                var auditSessionId = ((IIdentifiable)auditSessionSet[0]).Id;

                await storageService.StoreAsync(items);

                System.Console.Out.WriteLine($"Audit Session ID [{auditSessionId}]");
            });
        }

        public IStorageService StorageService { get; }


        private static List<IQuery> CreateAuditingSessionSet(string name, string email, string title, DateTime date)
        {
            var queries = new List<IQuery>();

            var auditSession = new AuditSession(title);
            queries.Add(auditSession);

            //var citation = new Citation("Audit Session_" + title, auditSession.Id);
            //queries.Add(citation);

            //var auditor = new Auditor(name, name, new List<char>(), email, citation.Id, auditSession.Id);
            //queries.Add(auditor);
            

            //var auditDate = new FuzzyDateTime(date, citation.Id, auditSession.Id);
            //queries.Add(auditDate);

            //queries.Add(new WhatHappenedWhen(auditSession.Id, auditDate.Id, citation.Id, auditSession.Id));
            //queries.Add(new WhoDidWhat(auditor.Id, auditSession.Id, citation.Id, auditSession.Id));

            return queries;
        }
    }
}
