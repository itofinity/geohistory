namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class Municipality : AbstractLocation
    {
        public const string Label = "municipality";

        public Municipality(string name, string publicationId, int startPage, int endPage, string auditSessionId) : base(name, Label, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
