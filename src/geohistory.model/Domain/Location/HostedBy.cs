namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Location
{
    public class HostedBy : Core.Relationship
    {
        public HostedBy(string entityId, string locationId, string citationId, string auditSessionId) : base(entityId, "hostedby", locationId, citationId, auditSessionId)
        {
        }
    }
}
