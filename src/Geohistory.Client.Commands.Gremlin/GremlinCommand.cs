using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel.Composition;
using System.Linq;
using Geohistory.Client.Commands.Api;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;

namespace UK.CO.Itofinity.GeoHistory.Client.Commands.Gremlin
{
    [Export(typeof(ICommand))]
    internal class GremlinCommand : AbstractCategoryCommand
    {
        public const string NAME = "gremlin";
        public const string DESCRIPTION = "err stuff to do with gremlin";

        [ImportingConstructor]
        public GremlinCommand([ImportMany] IEnumerable<IGremlinCommand> subCommands) : base(NAME, DESCRIPTION, subCommands)
        {
        }
    }
}   
