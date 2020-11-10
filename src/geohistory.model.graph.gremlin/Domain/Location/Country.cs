namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Country : AbstractLocation
    {
        public Country(string name, string publicationId, int startPage, int endPage, string auditSessionId) : base(name, "country", publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
