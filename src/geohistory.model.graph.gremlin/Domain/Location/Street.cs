namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Street : AbstractLocation
    {
        public Street(string name, string citationId, string auditSessionId) : base(name, "street", citationId, auditSessionId)
        {
        }
    }
}