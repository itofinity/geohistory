using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;

namespace Geohistory.Client.Commands.Api
{
    public abstract class AbstractCategoryCommand : Command
    {
        protected AbstractCategoryCommand(string name, string description, IEnumerable<ISubCommand> subCommands) : base(name, description)
        {
            this.Handler = CommandHandler.Create(() =>
            {
                /* do something */
                Console.WriteLine($"called {name}");

            });

            subCommands.ToList().ForEach(sc => this.Add((Symbol)sc));
        }
    }
}
