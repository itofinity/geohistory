namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Street : AbstractLocation
    {
        public Street(string name, string publicationId, int startPage, int endPage, string auditSessionId) : base(name, "street", publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
