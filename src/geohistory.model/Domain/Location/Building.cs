namespace Uk.Co.Itofinity.GeoHistory.Model.Domain.Location
{
    public class Building : AbstractLocation
    {
        public Building(int number, string citationId, string auditSessionId) : this($"{number}", citationId, auditSessionId)
        { }

        public Building(int startNumber, int endNumber, string citationId, string auditSessionId) : this($"{startNumber}-{endNumber}", citationId, auditSessionId)
        { }

        public Building(string name, string citationId, string auditSessionId) : base(name, "building", citationId, auditSessionId)
        {
        }
    }
}