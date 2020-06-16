using Uk.Co.Itofinity.Geohistory.Model.Audit;

namespace Uk.Co.Itofinity.Geohistory.Model.Audit
{
    public interface IAudited
    {
        IAudit Audit { get;  }
    }
}