namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Building : AbstractLocation
    {
        public Building(int number, string publicationId, int startPage, int endPage, string auditSessionId) : this($"{number}", publicationId, startPage, endPage, auditSessionId)
        { }

        public Building(int startNumber, int endNumber, string publicationId, int startPage, int endPage, string auditSessionId) : this($"{startNumber}-{endNumber}", publicationId, startPage, endPage, auditSessionId)
        { }

        public Building(string name, string publicationId, int startPage, int endPage, string auditSessionId) : base(name, "building", publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
