namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class HostedBy : Core.Relationship
    {
        public HostedBy(string entityId, string locationId, string publicationId, int startPage, int endPage, string auditSessionId) : base(entityId, "hostedby", locationId, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
