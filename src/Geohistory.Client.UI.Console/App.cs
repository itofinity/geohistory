using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli
{
    public class App
    {
        [ImportMany]
        public IEnumerable<ICommand> Commands;

        private CompositionContainer _container;

        public void Run(string[] args)
        {

            // Create a root command with some options
            var rootCommand = new RootCommand();


            Commands.ToList().ForEach(c => rootCommand.Add((Symbol)c));

            rootCommand.Handler = CommandHandler.Create(() =>
            {
                /* do something */
                Console.WriteLine("root");

            });

            rootCommand.InvokeAsync(args).Wait();

        }

        public App Compose()
        {
            //We create the catalog that will contain all the parts we want to compose (I.E. our plugins)
            var catalog = new AggregateCatalog();

            //We are looking in a directory for any exported parts
            //For this demo it will make our life easier to have the plugins at the app root.
            //What we'd typically do is have a "Plugins" folder at the app root that would
            //contain the plugins' DLL.
            var pluginsDir = AppDomain.CurrentDomain.BaseDirectory;
            catalog.Catalogs.Add(new DirectoryCatalog(pluginsDir, "*.dll"));

            //We create the CompositionContainer with the parts in the catalog.
            _container = new CompositionContainer(catalog);

            try
            {
                //We fill the current context's import with all the exports we found in the
                //catalog's parts. In this example, the property FormatReaders will be filled
                //with an instance of every class using the decorator [Export(IFormatReader)].
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
                throw;
            }

            return this;
        }
    }
}
