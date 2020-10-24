namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit
{
    public class AuditSession : AbstractIdentifiableEntity
    {
        public static readonly string TYPE = "auditsession";
        public AuditSession(string name) : base(name, TYPE)
        {
        }
    }
}
