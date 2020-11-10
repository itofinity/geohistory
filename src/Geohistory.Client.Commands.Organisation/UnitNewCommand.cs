using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel.Composition;
using System.Text;
using Geohistory.Client.Commands.Api;
using UK.CO.Geohistory.Client.Commands.Api;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Organisation.Military;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;

namespace UK.CO.Geohistory.Client.Commands.Organisation
{

    [Export(typeof(IUnitCommand))]
    public class UnitNewCommand : Command, IUnitCommand
    {
        public const string NAME = "new";
        public const string DESCRIPTION = "Add a new organisation";

        [ImportingConstructor]
        public UnitNewCommand(IStorageService storageService) : base(NAME, DESCRIPTION)
        {
            var nameOption = new Option<String>("--name");
            var sizeOption = new Option<String>("--size");
            var auditSessionIdOption = new HierarchicalOption<String>("GEOHISTORY", "--auditSessionId");
            var publicationIdOption = new HierarchicalOption<String>("GEOHISTORY", "--publicationId");
            var pageRangeOption = new Option<String>("--pageRange");

            this.Add(nameOption);
            this.Add(sizeOption);
            this.Add(auditSessionIdOption);
            this.Add(publicationIdOption);
            this.Add(pageRangeOption);

            this.Handler = CommandHandler.Create<string, string, string, string, string>(async (name, size, pageRange, publicationId, auditSessionId) => {
                var items = new List<IQuery>();

                publicationId = publicationIdOption.GetPrecedent(publicationId);
                auditSessionId = auditSessionIdOption.GetPrecedent(auditSessionId);
                //var citation = new Citation(tuple.Item1.Name, 1, auditSessionId);

                var unit = new Unit("1st Derbyshire Yeomanry", "regiment", publicationId, 1, 1, auditSessionId);
                items.Add(unit);

                await storageService.StoreAsync(items);
            });
        }
    }
}
