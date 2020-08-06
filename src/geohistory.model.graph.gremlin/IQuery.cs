using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model.Graph.Gremlin
{
    public interface IQuery
    {
        List<string> ToInsertQueries();
    }
}
