using System.Collections.Generic;
using System.Web;
using tinkerpop.scripts;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain
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
            entries.Add(GetIdentifiableScript().Build());
            return entries;
        }

        protected ScriptBuilder GetIdentifiableScript()
        {
            return GetNamedVertexScript()
                            .property("id", Id);
        }
    }
}
