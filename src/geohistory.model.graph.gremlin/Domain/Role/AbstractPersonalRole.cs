using System;
using System.Collections.Generic;
using System.Web;
using static tinkerpop.scripts.ScriptBuilder;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Role
{
    public abstract class AbstractPersonalRole : IPersonalRole
    {
        public AbstractPersonalRole(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public string Id => HttpUtility.UrlEncode(Name);

        public List<string> ToInsertQueries()
        {
            var entries = new List<string>();
            entries.Add(g.AddV("role")
                .property("id", Id)
                .property("name", Name)
                .property("pk", "pk").Build());
            return entries;
        }
    }
}
