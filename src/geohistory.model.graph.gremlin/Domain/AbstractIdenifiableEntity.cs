using System;
using System.Collections.Generic;
using System.Web;
using tinkerpop.scripts;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain
{
    public abstract class AbstractIdentifiableEntity : AbstractNamedEntity, IIdentifiable
    {
        protected AbstractIdentifiableEntity(string name, string type) : base(name, type)
        {
        }

        public string Id => HttpUtility.UrlEncode(Name.ToLower().Replace("&", "_and_"));

        public override List<string> ToInsertQueries()
        {
            var entries = new List<string>();
            entries.Add(GetAddIdentifiableScript().Build());
            return entries;
        }

        public override string ToFindQuery()
        {
            return GetFindIdentifiableScript().Build();
        }

        private ScriptBuilder GetFindIdentifiableScript()
        {
            return GetFindNamedVertexScript();
        }

        protected ScriptBuilder GetAddIdentifiableScript()
        {
            return GetAddNamedVertexScript()
                            .property("id", Id);
        }
    }
}
