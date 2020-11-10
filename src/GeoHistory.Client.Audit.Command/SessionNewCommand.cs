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
using System.ComponentModel.Composition;
using UK.CO.Geohistory.Client.Commands.Api;
using Microsoft.Extensions.Logging;

namespace UK.CO.Itofinity.GeoHistory.Client.Commands.Audit
{
    [Export(typeof(ISessionCommand))]
    public class SessionNewCommand : Command, ISessionCommand
    {
        public const string NAME = "new";
        public const string DESCRIPTION = "new session";

        [ImportingConstructor]
        public SessionNewCommand(IStorageService storageService, ILoggerFactory loggerFactory) : base(NAME, DESCRIPTION)
        {
            logger = loggerFactory.CreateLogger<SessionNewCommand>();
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            var userOption = new HierarchicalOption<string>("GEOHISTORY", "--user");
            var emailOption = new HierarchicalOption<string>("GEOHISTORY", "--email");
            this.Add(userOption);
            this.Add(emailOption);
            this.Add(new Option<string>("--title"));

            this.Handler = CommandHandler.Create<string, string, string>(async (user, email, title) =>
            {
                var items = new List<IQuery>();

                user = userOption.GetPrecedent(user);
                email = emailOption.GetPrecedent(email);

                var auditSessionSet = CreateAuditingSessionSet(user, email, title, DateTime.Now);
                items.AddRange(auditSessionSet);

                var auditSessionId = ((IIdentifiable)auditSessionSet[0]).Id;

                await storageService.StoreAsync(items);

                Console.Out.WriteLine($"Added auditing session for '{user}/{email}' with id '{auditSessionId}'");
                logger.LogError("by jove!");
            });
        }

        public IStorageService StorageService { get; }
        private readonly ILogger logger;


        private static List<IQuery> CreateAuditingSessionSet(string name, string email, string title, DateTime date)
        {
            var queries = new List<IQuery>();

            var auditSession = new AuditSession(title);
            queries.Add(auditSession);

//            var citation = new Citation("Audit Session_" + title, auditSession.Id);
//            queries.Add(citation);

            var auditor = new Auditor(name, email, auditSession.Id);
            queries.Add(auditor);
            

            var auditDate = new FuzzyDateTime(date, auditSession.Id);
            queries.Add(auditDate);

            queries.Add(new AuditSessionWhen(auditSession.Id, auditDate.Id));
            queries.Add(new WhoAuditSession(auditor.Id, auditSession.Id));

            return queries;
        }
    }
}
