using System;
using System.Collections.Generic;
using tinkerpop.scripts;
using static tinkerpop.scripts.ScriptBuilder;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain
{
    public abstract class AbstractEntity : IQuery, ITyped
    {
        public AbstractEntity(string type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public string Type { get; }

        public string ToFindQuery()
        {
            throw new NotImplementedException();
        }

        public virtual List<string> ToInsertQueries()
        {
            var entries = new List<string>();
            entries.Add(GetTypedVertexScript().Build());
            return entries;
        }

        protected ScriptBuilder GetTypedVertexScript()
        {
            return g.AddV(Type)
                            .property("pk", "pk");
        }
    }
}
