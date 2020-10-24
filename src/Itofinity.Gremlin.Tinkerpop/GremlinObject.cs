using System;
using System.Collections.Generic;
using System.Text;

namespace Itofinity.Gremlin.Tinkerpop
{

    // {"id":"1st+derbyshire+yeomanry+scrapbook+1939+-+1947","label":"book","type":"vertex","properties":{"name":[{"id":"79b13e4d-e18c-4ee3-851d-abb1090e3adc","value":"1st Derbyshire Yeomanry Scrapbook 1939 - 1947"}],"pk":[{"id":"1st+derbyshire+yeomanry+scrapbook+1939+-+1947|pk","value":"pk"}]}}
    public class GremlinObject
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        // TODO shouldn't be dynmaic
        public Dictionary<string, List<dynamic>> Properties { get; set;  }
    }
}
