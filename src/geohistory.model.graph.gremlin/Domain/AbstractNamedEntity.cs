using System;
using System.Collections.Generic;
using tinkerpop.scripts;
using static tinkerpop.scripts.ScriptBuilder;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain
{
    public abstract class AbstractNamedEntity : IQuery, INamed, ITyped
    {
        public AbstractNamedEntity(string name, string type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public string Name { get; }

        public string Type { get; }

        public virtual List<string> ToInsertQueries()
        {
            var entries = new List<string>();
            entries.Add(GetNamedVertexScript().Build());
            return entries;
        }

        protected ScriptBuilder GetNamedVertexScript()
        {
            return g.AddV(Type)
                            .property("name", Name)
                            .property("pk", "pk");
        }
    }
}
