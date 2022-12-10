namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Time
{
    public class KeyDateTime : AbstractCitedAuditedEntity
    {
        public KeyDateTime(string name, string citationId, string auditSessionId) : base(name, "keydatetime", citationId, auditSessionId)
        {
        }
    }
}
