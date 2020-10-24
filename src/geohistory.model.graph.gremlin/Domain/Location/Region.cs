namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Region : AbstractLocation
    {
        public Region(string name, string citationId, string auditSessionId) : base(name, "region", citationId, auditSessionId)
        {
        }
    }
}