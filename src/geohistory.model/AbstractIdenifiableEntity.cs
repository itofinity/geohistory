using System.Web;

namespace Uk.Co.Itofinity.GeoHistory.Model
{
    public abstract class AbstractIdentifiableEntity : AbstractNamedEntity, IIdentifiable
    {
        protected AbstractIdentifiableEntity(string name, string type) : base(name, type)
        {
        }

        public string Id => HttpUtility.UrlEncode(Name.ToLower().Replace("&", "_and_"));
    }
}
