namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Location
{
    public class Street : AbstractLocation
    {
        public Street(string name, string citationId, string auditSessionId) : base(name, "street", citationId, auditSessionId)
        {
        }
    }
}