using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using Geohistory.Client.Commands.Api;

namespace UK.CO.Geohistory.Client.Commands.Organisation
{
    [Export(typeof(IOrganisationCommand))]
    public class UnitCommand : AbstractCategoryCommand, IOrganisationCommand
    {
        public const string NAME = "unit";
        public const string DESCRIPTION = "Commands relating to Military Units";

        [ImportingConstructor]
        public UnitCommand([ImportMany] IEnumerable<IUnitCommand> subCommands) : base(NAME, DESCRIPTION, subCommands)
        {
        }
    }
}
