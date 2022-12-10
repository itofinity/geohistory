namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Audit
{
    public class AuditSession : AbstractIdentifiableEntity
    {
        public AuditSession(string name) : base(name, "auditsession")
        {
        }
    }
}
