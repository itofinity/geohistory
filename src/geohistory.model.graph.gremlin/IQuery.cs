using System.Collections.Generic;

namespace UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin
{
    public interface IQuery
    {
        string ToFindQuery();
        List<string> ToInsertQueries();
    }
}
