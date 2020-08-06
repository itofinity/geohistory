using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location
{
    public class LatitudeLongtitude : AbstractLocation
    {
        public LatitudeLongtitude(double latitude, double longtitude, string citationId, string auditSessionId) : base($"{latitude},{longtitude}", "latitudelongtitude", new Dictionary<string, object>() { { "latitude", $"{latitude}" }, { "longtitude", $"{longtitude}" } }, citationId, auditSessionId)
        {
        }
    }
}
