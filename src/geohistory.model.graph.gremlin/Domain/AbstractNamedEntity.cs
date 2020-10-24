using System;
using System.Collections.Generic;
using Itofinity.Geohistory.Spi.Domain;
using tinkerpop.scripts;
using static tinkerpop.scripts.ScriptBuilder;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain
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
            entries.Add(GetAddNamedVertexScript().Build());
            return entries;
        }

        public virtual string ToFindQuery()
        {
            return GetFindNamedVertexScript().Build();
        }

        protected ScriptBuilder GetAddNamedVertexScript()
        {
            return g.AddV(Type)
                            .property("name", Name)
                            .property("pk", "pk");
        }

        protected ScriptBuilder GetFindNamedVertexScript()
        {
            return g.V().hasLabel(Type).has("name", ScriptClauses.value(Name));
        }
    }
}
