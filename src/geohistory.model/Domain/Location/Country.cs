namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Location
{
    public class Country : AbstractLocation
    {
        public Country(string name, string citationId, string auditSessionId) : base(name, "country", citationId, auditSessionId)
        {
        }
    }
}
