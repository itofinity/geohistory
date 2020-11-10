namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Region : AbstractLocation
    {
        public Region(string name, string publicationId, int startPage, int endPage, string auditSessionId) : base(name, "region", publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
