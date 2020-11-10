using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using Microsoft.Extensions.Logging;

namespace UK.CO.Itofinity.GeoHistory.Client.UI.Cli
{
    [Export(typeof(ILoggerFactory))]
    public class DefaultLoggerFactory : LoggerFactory
    {
    }
}
