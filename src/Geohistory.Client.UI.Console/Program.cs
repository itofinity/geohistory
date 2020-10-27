using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using UK.CO.Itofinity.GeoHistory.Client.Api.Service;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            new App()
                .Compose()
                .Run(args);
        }
    }
}
