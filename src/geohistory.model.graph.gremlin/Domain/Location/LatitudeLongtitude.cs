using System;
using System.Collections.Generic;
using System.Text;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class LatitudeLongtitude : AbstractLocation
    {
        public LatitudeLongtitude(double latitude, double longtitude, string publicationId, int startPage, int endPage, string auditSessionId) : base($"{latitude},{longtitude}", "latitudelongtitude", new Dictionary<string, object>() { { "latitude", $"{latitude}" }, { "longtitude", $"{longtitude}" } }, publicationId, startPage, endPage, auditSessionId)
        {
        }
    }
}
