using System;
using System.Collections.Generic;
using System.CommandLine;
using System.ComponentModel.Composition;
using System.Text;
using Geohistory.Client.Commands.Api;

namespace UK.CO.Geohistory.Client.Commands.Organisation
{
    [Export(typeof(ICommand))]
    public class OrganisationCommand : AbstractCategoryCommand
    {
        public const string NAME = "organisation";
        public const string DESCRIPTION = "Commands relating to Organisations";

        [ImportingConstructor]
        public OrganisationCommand([ImportMany]IEnumerable<IOrganisationCommand> subCommands) : base(NAME, DESCRIPTION, subCommands)
        {
        }
    }
}
