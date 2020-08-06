namespace Uk.Co.Itofinity.GeoHistory.Model.Audit
{
    public interface IAudited
    {
        IAudit Audit { get; }
    }
}